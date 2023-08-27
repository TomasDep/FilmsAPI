using System.Security.Claims;
using AutoMapper;
using FilmsAPI.Core.Helpers;
using FilmsAPI.Dao;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FilmsAPI.Core.Services
{
    public class ReviewServicesImpl : IReviewServices
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewServicesImpl> _logger;

        public ReviewServicesImpl(
            IReviewRepository reviewRepository,
            IMovieRepository movieRepository,
            IMapper mapper,
            ApplicationDbContext context,
            ILogger<ReviewServicesImpl> logger
        )
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult> CreateReview(
            long movieId,
            AddReviewDto addReviewDto,
            HttpContext httpContext
        )
        {
            try
            {
                string userId = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                bool existReview = await _reviewRepository.IsMovieByMovieIdAndUserId(movieId, userId);
                if (!existReview)
                {
                    return new BadRequestObjectResult("The user has already written a review of this movie");
                }
                Review review = _mapper.Map<Review>(addReviewDto);
                review.MovieId = movieId;
                review.UserId = userId;
                _context.Add(review);
                await _context.SaveChangesAsync();
                return new ObjectResult("") { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<List<ReviewDto>>> GetReviewsPaginate(
            long movieId,
            PaginationDto paginationDto,
            HttpContext httpContext
        )
        {
            try
            {
                var queryable = _context.Reviews.Include(review => review.User).AsQueryable();
                await httpContext.InsertParameterPagination(queryable, paginationDto.NumberRecordsPerPage);
                var reviews = await queryable.Where(review => review.MovieId == movieId)
                    .Paginate(paginationDto)
                    .ToListAsync();
                var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);
                return new OkObjectResult(reviewsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveReview(long reviewId, HttpContext httpContext)
        {
            Review reviewDb = await _reviewRepository.GetReviewById(reviewId);
            if (reviewDb == null)
            {
                return new NotFoundObjectResult($"Review by Id: {reviewId} not found");
            }
            string userId = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            if (reviewDb.UserId != userId)
            {
                return new ForbidResult();
            }
            _context.Remove(reviewDb);
            await _context.SaveChangesAsync();
            return new ObjectResult("") { StatusCode = 204 };
        }

        public async Task<ActionResult> UpdateReview(
            long movieId,
            long reviewId,
            UpdateReviewDto updateReviewDto,
            HttpContext httpContext
        )
        {
            try
            {
                Review reviewDb = await _reviewRepository.GetReviewById(reviewId);
                if (reviewDb == null)
                {
                    return new NotFoundObjectResult($"Review by Id: {reviewId} not found");
                }
                string userId = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                if (reviewDb.UserId != userId)
                {
                    return new ObjectResult("The user does not have permission to edit this review") { StatusCode = 403 };
                }
                reviewDb = _mapper.Map(updateReviewDto, reviewDb);
                await _context.SaveChangesAsync();
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}