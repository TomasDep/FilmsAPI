using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class MoviePatchDto
    {
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool IsCinema { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}