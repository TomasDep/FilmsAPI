using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class GenreDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}