using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dao.Entities
{
    public class Movie
    {
        public long Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool IsCinema { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
        public List<MoviesGenres> MoviesGenres { get; set; }
    }
}