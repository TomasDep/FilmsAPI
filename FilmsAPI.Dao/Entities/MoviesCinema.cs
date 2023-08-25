using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dao.Entities
{
    public class MoviesCinema
    {
        public long MovieId { get; set; }
        public long CinemaId { get; set; }
        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }
    }
}