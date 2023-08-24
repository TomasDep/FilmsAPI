using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dao.Entities
{
    public class Actor
    {
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Photo { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
    }
}