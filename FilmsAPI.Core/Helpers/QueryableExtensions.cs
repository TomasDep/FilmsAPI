using FilmsAPI.Dto;

namespace FilmsAPI.Core.Helpers
{
    public static class QueryableExtesions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto paginationDto)
        {
            return queryable.Skip((paginationDto.Page - 1) * paginationDto.NumberRecordsPerPage)
                .Take(paginationDto.NumberRecordsPerPage);
        }
    }
}