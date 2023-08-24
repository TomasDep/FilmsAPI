namespace FilmsAPI.Dao.Entities
{
    public class MoviesGenres
    {
        public long GenreId { get; set; }
        public long MovieId { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}