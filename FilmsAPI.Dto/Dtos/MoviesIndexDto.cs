namespace FilmsAPI.Dto
{
    public class MoviesIndexDto
    {
        public List<MovieDto> FutureRelease { get; set; }
        public List<MovieDto> InTheaters { get; set; }
    }
}