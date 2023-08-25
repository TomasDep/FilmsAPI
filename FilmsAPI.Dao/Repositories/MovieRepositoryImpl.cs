using FilmsAPI.Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Dao.Repositories
{
    public class MovieRepositoryImpl : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> CollectionsMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<bool> CreateMovie(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Movie>> GetMoviesInCinemas(int movies)
        {
            return await _context.Movies.Where(movie => movie.IsCinema)
                .Take(movies)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetNextReleases(int movies, DateTime date)
        {
            return await _context.Movies.Where(movie => movie.ReleaseDate > date)
                .OrderBy(orderBy => orderBy.ReleaseDate)
                .Take(movies)
                .ToListAsync();
        }

        public async Task<bool> IsMovieById(long id)
        {
            return await _context.Movies.AnyAsync(movie => movie.Id == id);
        }

        public async Task<Movie> MovieById(long id)
        {
            return await _context.Movies
                .Include(movie => movie.MoviesActors).ThenInclude(x => x.Actor)
                .Include(movie => movie.MoviesGenres).ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync(movie => movie.Id == id);
        }

        public async Task<bool> RemoveMovie(Movie movie)
        {
            _context.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}