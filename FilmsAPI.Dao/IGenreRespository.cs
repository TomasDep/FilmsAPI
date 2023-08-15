using FilmsAPI.Dao.Entities;

namespace FilmsAPI.Dao
{
    public interface IGenreRepository
    {
        Task<List<Genre>> CollectionsGenres();
        Task<Genre> GenreById(long id);
        void CreateGenre(Genre genre);
        void UpdateGenre(Genre genre);
        Task<bool> IsGenreById(long id);
        void RemoveGenre(long id);
    }
}