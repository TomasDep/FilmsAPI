using FilmsAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FilmsAPI.Core
{
    public interface IActorServices
    {
        Task<ActionResult<List<ActorDto>>> CollectionActors();
        Task<ActionResult<List<ActorDto>>> CollectionActorsPaginate(PaginationDto paginationDto, HttpContext httpContext);
        Task<ActionResult<ActorDto>> GetActorById(long id);
        Task<ActionResult> CreateActor(AddActorDto addActorDto);
        Task<ActionResult> UpdateActor(long id, UpdateActorDto updateActorDto);
        Task<ActionResult> RemoveActor(long id);
        Task<ActionResult> PatchActor(long id, JsonPatchDocument<ActorPatchDto> patchDocument, ModelStateDictionary modelState);
    }
}