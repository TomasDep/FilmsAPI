using FilmsAPI.Dao.Entities;

namespace FilmsAPI.Dao
{
    public interface IReviewRepository
    {
        Task<bool> IsMovieByMovieIdAndUserId(long movieId, string userId);
        Task<Review> GetReviewById(long reviewId);
    }
}