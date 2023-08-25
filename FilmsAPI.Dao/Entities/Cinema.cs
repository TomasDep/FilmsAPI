using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dao.Entities
{
    public class Cinema
    {
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public List<MoviesCinema> MoviesCinema { get; set; }
    }
}