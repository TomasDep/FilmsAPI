using FilmsAPI.Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Dao.Repositories
{
    public class ActorRepositoryImpl : IActorRepository
    {
        private readonly ApplicationDbContext _context;

        public ActorRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Actor>> CollectionActors()
        {
            var actors = await _context.Actors.ToListAsync();
            return actors;
        }

        public async Task<Actor> GetActorById(long id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.Id == id);
            return actor;
        }

        public async Task<bool> CreateActor(Actor actor)
        {
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsActorById(long id)
        {
            return await _context.Actors.AnyAsync(actor => actor.Id == id);
        }

        public async Task<bool> RemoveActor(Actor actor)
        {
            _context.Remove(actor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}