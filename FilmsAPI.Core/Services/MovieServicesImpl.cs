using AutoMapper;
using FilmsAPI.Core.Helpers;
using FilmsAPI.Dao;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace FilmsAPI.Core.Services
{
    public class MovieServicesImpl : IMovieServices
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IStorageFiles _storageFiles;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovieServicesImpl> _logger;
        private readonly string _container = "moviesContainer";

        public MovieServicesImpl(
            IMapper mapper,
            IMovieRepository movieRepository,
            IStorageFiles storageFiles,
            ApplicationDbContext context,
            ILogger<MovieServicesImpl> logger
        )
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _storageFiles = storageFiles;
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult<List<MovieDto>>> AllMoviesWithFilter(MovieFilterDto movieFilterDto, HttpContext httpContext)
        {
            try
            {
                var moviesQueryable = _context.Movies.AsQueryable();
                if (!string.IsNullOrEmpty(movieFilterDto.Title))
                {
                    moviesQueryable = moviesQueryable.Where(movie => movie.Title.Contains(movieFilterDto.Title));
                }
                if (movieFilterDto.IsCinema)
                {
                    moviesQueryable = moviesQueryable.Where(movie => movie.IsCinema);
                }
                if (movieFilterDto.NextReleases)
                {
                    DateTime today = DateTime.Today;
                    moviesQueryable = moviesQueryable.Where(movie => movie.ReleaseDate > today);
                }
                if (movieFilterDto.GenreId != 0)
                {
                    moviesQueryable = moviesQueryable
                        .Where(movie => movie.MoviesGenres.Select(mg => mg.GenreId).Contains(movieFilterDto.GenreId));
                }
                if (!string.IsNullOrEmpty(movieFilterDto.OrderBy))
                {
                    var typeOrder = movieFilterDto.OrderAsc ? "ascending" : "descending";
                    moviesQueryable = moviesQueryable.OrderBy($"{movieFilterDto.OrderBy} {typeOrder}");
                }
                await httpContext.InsertParameterPagination(moviesQueryable, movieFilterDto.NumberRecordsPerPage);
                var movies = await moviesQueryable.Paginate(movieFilterDto.Pagination).ToListAsync();
                var moviesDto = _mapper.Map<MovieDto>(movies);
                return new OkObjectResult(moviesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<List<MovieDto>>> CollectionMovies()
        {
            try
            {
                int top = 5;
                DateTime today = DateTime.Today;
                var nextReleases = await _movieRepository.GetNextReleases(top, today);
                var inTheaters = await _movieRepository.GetMoviesInCinemas(top);
                var movies = await _movieRepository.CollectionsMovies();
                var moviesDto = _mapper.Map<List<MovieDto>>(movies);
                return new OkObjectResult(moviesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public Task<ActionResult<List<MovieDto>>> CollectionMoviesPaginate(PaginationDto paginationDto, HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> CreateMovie(AddMovieDto addMovieDto)
        {
            try
            {
                var movie = _mapper.Map<Movie>(addMovieDto);
                if (addMovieDto.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await addMovieDto.Poster.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(addMovieDto.Poster.FileName);
                        movie.Poster = await _storageFiles.SaveFile(content, extension, _container, addMovieDto.Poster.ContentType);
                    }
                }
                AssignOrderActors(movie);
                await _movieRepository.CreateMovie(movie);
                var movieDto = _mapper.Map<MovieDto>(movie);
                return new ObjectResult(movieDto) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<MovieDetailsDto>> GetMovieById(long id)
        {
            try
            {
                var movie = await _movieRepository.MovieById(id);
                if (movie != null)
                {
                    return new NotFoundObjectResult($"Movie by id: {id} not exists.");
                }
                movie.MoviesActors = movie.MoviesActors.OrderBy(x => x.Orden).ToList();
                var movieDto = _mapper.Map<MovieDetailsDto>(movie);
                return new OkObjectResult(movieDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> PatchMovie(long id, JsonPatchDocument<MoviePatchDto> patchDocument, ModelStateDictionary modelState)
        {
            try
            {
                if (patchDocument == null)
                    return new BadRequestObjectResult(patchDocument);
                var movie = await _movieRepository.MovieById(id);
                if (movie == null)
                    return new NotFoundObjectResult($"Movie by id: {id} not exists.");
                var movieDto = _mapper.Map<MoviePatchDto>(movie);
                patchDocument.ApplyTo(movieDto, modelState);
                _mapper.Map(movieDto, movie);
                await _context.SaveChangesAsync();
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveMovie(long id)
        {
            try
            {
                var movie = await _movieRepository.MovieById(id);
                if (movie == null)
                    return new NotFoundObjectResult($"Movie by id: {id} not exists.");
                if (movie.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await _storageFiles.RemoveFile(movie.Poster, _container);
                    }
                }
                await _movieRepository.RemoveMovie(movie);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<List<MoviesIndexDto>>> UpcomingReleasesMovies()
        {
            try
            {
                int top = 5;
                DateTime today = DateTime.Today;
                var nextReleases = await _movieRepository.GetNextReleases(top, today);
                var inTheaters = await _movieRepository.GetMoviesInCinemas(top);
                var result = new MoviesIndexDto();
                result.FutureRelease = _mapper.Map<List<MovieDto>>(nextReleases);
                result.InTheaters = _mapper.Map<List<MovieDto>>(inTheaters);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> UpdateMovie(long id, UpdateMovieDto updateMovieDto)
        {
            try
            {
                var movie = await _movieRepository.MovieById(id);
                if (movie == null)
                    return new NotFoundObjectResult($"Movie by id: {id} not exists.");
                movie.Id = id;
                movie.Title = updateMovieDto.Title;
                movie.IsCinema = updateMovieDto.IsCinema;
                movie.ReleaseDate = updateMovieDto.ReleaseDate;
                if (updateMovieDto.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await updateMovieDto.Poster.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(updateMovieDto.Poster.FileName);
                        movie.Poster = await _storageFiles.UpdateFile(
                            content,
                            extension,
                            _container,
                            movie.Poster,
                            updateMovieDto.Poster.ContentType
                        );
                    }
                }
                AssignOrderActors(movie);
                await _movieRepository.UpdateMovie(movie);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        private void AssignOrderActors(Movie movie)
        {
            if (movie.MoviesActors != null)
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                    movie.MoviesActors[i].Orden = i;
        }
    }
}