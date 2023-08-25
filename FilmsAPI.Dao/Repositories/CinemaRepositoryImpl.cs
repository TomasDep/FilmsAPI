using FilmsAPI.Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Dao.Repositories
{
    public class CinemaRepositoryImpl : ICinemaRepository
    {
        private readonly ApplicationDbContext _context;

        public CinemaRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Cinema>> CollectionCinemas()
        {
            var cinema = await _context.Cinema.ToListAsync();
            return cinema;
        }

        public async Task<Cinema> GetCinemaById(long id)
        {
            var cinema = await _context.Cinema.FirstOrDefaultAsync(cinema => cinema.Id == id);
            return cinema;
        }

        public async Task<bool> CreateCinema(Cinema cinema)
        {
            _context.Add(cinema);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCinema(Cinema cinema)
        {
            _context.Entry(cinema).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsCinemaById(long id)
        {
            return await _context.Cinema.AnyAsync(cinema => cinema.Id == id);
        }

        public async Task<bool> RemoveCinema(Cinema cinema)
        {
            _context.Remove(cinema);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}