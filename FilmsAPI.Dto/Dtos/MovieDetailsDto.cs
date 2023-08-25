namespace FilmsAPI.Dto
{
    public class MovieDetailsDto : MovieDto
    {
        public List<GenreDto> Genres { get; set; }
        public List<ActorMovieDetailDto> Actors { get; set; }
    }
}