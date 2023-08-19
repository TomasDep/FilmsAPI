namespace FilmsAPI.Dto
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        private int _recordsPerPage = 10;
        private readonly int _maxRecordsPerPage = 50;

        public int NumberRecordsPerPage
        {
            get => _recordsPerPage;
            set
            {
                _recordsPerPage = (value > _maxRecordsPerPage) ?
                    _maxRecordsPerPage : value;
            }
        }
    }
}