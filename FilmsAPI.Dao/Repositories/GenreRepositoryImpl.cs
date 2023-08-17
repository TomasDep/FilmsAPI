using FilmsAPI.Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Dao.Repositories
{
    public class GenreRepositoryImpl : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> CollectionsGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GenreById(long id)
        {
            return await _context.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
        }

        public async Task<bool> CreateGenre(Genre genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGenre(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsGenreById(long id)
        {
            return await _context.Genres.AnyAsync(genre => genre.Id == id);
        }

        public async Task<bool> RemoveGenre(long id)
        {
            _context.Remove(new Genre { Id = id });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}