using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Core.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParameterPagination<T>(
            this HttpContext httpContext,
            IQueryable<T> queryable,
            int recordsPerPage
        )
        {
            double records = await queryable.CountAsync();
            double recordsPages = Math.Ceiling(records / recordsPerPage);
            httpContext.Response.Headers.Add("recordsPages", recordsPages.ToString());
        }
    }
}