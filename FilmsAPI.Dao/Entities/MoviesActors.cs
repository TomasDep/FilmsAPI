namespace FilmsAPI.Dao.Entities
{
    public class MoviesActors
    {
        public long ActorId { get; set; }
        public long MovieId { get; set; }
        public string Character { get; set; }
        public int Orden { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}