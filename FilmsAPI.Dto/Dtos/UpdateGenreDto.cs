using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class UpdateGenreDto
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}