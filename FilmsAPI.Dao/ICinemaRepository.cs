using FilmsAPI.Dao.Entities;

namespace FilmsAPI.Dao
{
    public interface ICinemaRepository
    {
        Task<List<Cinema>> CollectionCinemas();
        Task<Cinema> GetCinemaById(long id);
        Task<bool> CreateCinema(Cinema cinema);
        Task<bool> UpdateCinema(Cinema cinema);
        Task<bool> IsCinemaById(long id);
        Task<bool> RemoveCinema(Cinema cinema);
    }
}