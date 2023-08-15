using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dao.Entities
{
    public class Genre
    {
        public long Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}