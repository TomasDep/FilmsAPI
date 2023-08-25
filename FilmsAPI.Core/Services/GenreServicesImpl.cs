using AutoMapper;
using FilmsAPI.Dao;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilmsAPI.Core.Services
{
    public class GenreServicesImpl : IGenreServices
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<GenreServicesImpl> _logger;

        public GenreServicesImpl(
            IMapper mapper,
            IGenreRepository genreRepository,
            ILogger<GenreServicesImpl> logger
        )
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
            _logger = logger;
        }

        public async Task<ActionResult<List<GenreDto>>> CollectionGenres()
        {
            try
            {
                var genreCollection = await _genreRepository.CollectionsGenres();
                var genreDto = _mapper.Map<List<GenreDto>>(genreCollection);
                return new OkObjectResult(genreDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<GenreDto>> GenreById(long id)
        {
            try
            {
                var genre = await _genreRepository.GenreById(id);
                if (genre == null)
                    return new NotFoundObjectResult($"Genre by Id: {id} not exist");
                var genreDto = _mapper.Map<GenreDto>(genre);
                return genreDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> AddGenre(AddGenreDto addGenreDto)
        {
            try
            {
                var newGenre = _mapper.Map<Genre>(addGenreDto);
                await _genreRepository.CreateGenre(newGenre);
                var newGenreDto = _mapper.Map<GenreDto>(newGenre);
                return new ObjectResult(newGenreDto) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> UpdateGenre(long id, UpdateGenreDto updateGenreDto)
        {
            try
            {
                var isGenre = await _genreRepository.IsGenreById(id);
                if (!isGenre)
                    return new NotFoundObjectResult($"Genre by Id: {id} not exist");
                var genre = _mapper.Map<Genre>(updateGenreDto);
                genre.Id = id;
                await _genreRepository.UpdateGenre(genre);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveGenre(long id)
        {
            try
            {
                var isGenre = await _genreRepository.IsGenreById(id);
                if (!isGenre)
                    return new NotFoundObjectResult($"Genre by Id: {id} not exist");
                await _genreRepository.RemoveGenre(id);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}