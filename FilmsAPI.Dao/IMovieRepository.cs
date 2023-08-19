using FilmsAPI.Dao.Entities;

namespace FilmsAPI.Dao
{
    public interface IMovieRepository
    {
        Task<List<Movie>> CollectionsMovies();
        Task<Movie> MovieById(long id);
        Task<bool> CreateMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> IsMovieById(long id);
        Task<bool> RemoveMovie(Movie movie);
    }
}