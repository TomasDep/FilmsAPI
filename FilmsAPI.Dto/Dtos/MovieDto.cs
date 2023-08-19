using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class MovieDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsCinema { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
    }
}