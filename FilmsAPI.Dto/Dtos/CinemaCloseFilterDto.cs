using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class CinemaCinemaFilterDto
    {
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
        private int _distanceInKm = 10;
        private int _distanceMaxKm = 50;
        public int DistanceInKm
        {
            get
            {
                return _distanceInKm;
            }
            set
            {
                _distanceInKm = (value > _distanceMaxKm) ? _distanceMaxKm : value;
            }
        }
    }
}