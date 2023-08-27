namespace FilmsAPI.Dto
{
    public class ReviewDto
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public long MovieId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}