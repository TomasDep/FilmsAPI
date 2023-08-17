using FilmsAPI.Dao.Entities;

namespace FilmsAPI.Dao
{
    public interface IGenreRepository
    {
        Task<List<Genre>> CollectionsGenres();
        Task<Genre> GenreById(long id);
        Task<bool> CreateGenre(Genre genre);
        Task<bool> UpdateGenre(Genre genre);
        Task<bool> IsGenreById(long id);
        Task<bool> RemoveGenre(long id);
    }
}