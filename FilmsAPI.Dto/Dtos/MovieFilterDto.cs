
namespace FilmsAPI.Dto
{
    public class MovieFilterDto
    {
        public int Page { get; set; } = 1;
        public int NumberRecordsPerPage { get; set; } = 10;
        public PaginationDto Pagination
        {
            get { return new PaginationDto() { Page = Page, NumberRecordsPerPage = NumberRecordsPerPage }; }
        }

        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool IsCinema { get; set; }
        public bool NextReleases { get; set; }
        public string OrderBy { get; set; }
        public bool OrderAsc { get; set; }
    }
}