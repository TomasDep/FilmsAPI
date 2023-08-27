using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Dto
{
    public class AddReviewDto
    {
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }
    }
}