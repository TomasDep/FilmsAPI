using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FilmsAPI.Dao.Entities
{
    public class Review
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }
        public long MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}