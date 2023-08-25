using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class UpdateCinemaDto
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
    }
}