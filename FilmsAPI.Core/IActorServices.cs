using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Core
{
    public interface IActorServices
    {
        Task<ActionResult<List<ActorDto>>> CollectionActors();
        Task<ActionResult<ActorDto>> GetActorById(long id);
        Task<ActionResult> CreateActor(AddActorDto addActorDto);
        Task<ActionResult> UpdateActor(long id, UpdateActorDto updateActorDto);
        Task<ActionResult> RemoveActor(long id);
    }
}