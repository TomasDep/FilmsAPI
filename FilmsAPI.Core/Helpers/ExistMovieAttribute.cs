using FilmsAPI.Dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Core.Helpers
{
    public class ExistMovieAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly ApplicationDbContext _context;

        public ExistMovieAttribute(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var movieIdObject = context.HttpContext.Request.RouteValues["movieId"];
            if (movieIdObject == null)
            {
                return;
            }
            long movieId = long.Parse(movieIdObject.ToString());
            bool existMovie = await _context.Movies.AnyAsync(movie => movie.Id == movieId);
            if (!existMovie)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }
    }
}