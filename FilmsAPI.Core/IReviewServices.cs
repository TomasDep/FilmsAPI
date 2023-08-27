using FilmsAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Core
{
    public interface IReviewServices
    {
        Task<ActionResult<List<ReviewDto>>> GetReviewsPaginate(
            long movieId,
            PaginationDto paginationDto,
            HttpContext httpContext
        );
        Task<ActionResult> CreateReview(
            long movieId,
            AddReviewDto addReviewDto,
            HttpContext httpContext
        );
        Task<ActionResult> UpdateReview(
            long movieId,
            long reviewId,
            UpdateReviewDto updateReviewDto,
            HttpContext httpContext
        );
        Task<ActionResult> RemoveReview(long reviewId, HttpContext httpContext);
    }
}