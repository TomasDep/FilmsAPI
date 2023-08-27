using FilmsAPI.Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Dao.Repositories
{
    public class ReviewRepositoryImpl : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsMovieByMovieIdAndUserId(long movieId, string userId)
        {
            return await _context.Reviews
                .AnyAsync(review => review.MovieId == movieId && review.UserId == userId);
        }

        public async Task<Review> GetReviewById(long reviewId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(review => review.Id == reviewId);
        }
    }
}