using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace FilmsAPI.Dao.Entities
{
    public class Cinema
    {
        public long Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public Point Location { get; set; }
        public List<MoviesCinema> MoviesCinema { get; set; }
    }
}