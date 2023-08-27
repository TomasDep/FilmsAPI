using FilmsAPI.Core;
using FilmsAPI.Core.Helpers;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [Route("api/movies/{movieId:long}/reviews")]
    [ApiController]
    [ServiceFilter(typeof(ExistMovieAttribute))]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpGet]
        public Task<ActionResult<List<ReviewDto>>> GetReviewsPaginate(
            long movieId,
            [FromQuery] PaginationDto paginationDto
        )
        {
            return _reviewServices.GetReviewsPaginate(movieId, paginationDto, HttpContext);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Task<ActionResult> CreateReview(long movieId, [FromBody] AddReviewDto addReviewDto)
        {
            return _reviewServices.CreateReview(movieId, addReviewDto, HttpContext);
        }

        [HttpPut("reviewId:long")]
        public Task<ActionResult> UpdateReview(
            long movieId,
            long reviewId,
            [FromBody] UpdateReviewDto updateReviewDto
        )
        {
            return _reviewServices.UpdateReview(movieId, reviewId, updateReviewDto, HttpContext);
        }

        [HttpDelete("{reviewId:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Task<ActionResult> RemoveReview(long reviewId)
        {
            return _reviewServices.RemoveReview(reviewId, HttpContext);
        }
    }
}