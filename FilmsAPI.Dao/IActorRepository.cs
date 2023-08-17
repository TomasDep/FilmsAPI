using FilmsAPI.Dao.Entities;

namespace FilmsAPI.Dao
{
    public interface IActorRepository
    {
        Task<List<Actor>> CollectionActors();
        Task<Actor> GetActorById(long id);
        Task<bool> CreateActor(Actor actor);
        Task<bool> UpdateActor(Actor actor);
        Task<bool> IsActorById(long id);
        Task<bool> RemoveActorById(long id);
    }
}