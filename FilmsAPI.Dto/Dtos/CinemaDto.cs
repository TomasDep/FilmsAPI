using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class CinemaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}