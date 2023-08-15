using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class AddGenreDto
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}