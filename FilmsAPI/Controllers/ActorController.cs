using FilmsAPI.Core;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly IActorServices _actorServices;

        public ActorController(IActorServices actorServices)
        {
            _actorServices = actorServices;
        }

        [HttpGet]
        public Task<ActionResult<List<ActorDto>>> GetCollectionActors()
        {
            return _actorServices.CollectionActors();
        }

        [HttpGet("paginate")]
        public Task<ActionResult<List<ActorDto>>> GetCollectionActorsPaginate([FromQuery] PaginationDto paginationDto)
        {
            return _actorServices.CollectionActorsPaginate(paginationDto, HttpContext);
        }

        [HttpGet("{id:long}")]
        public Task<ActionResult<ActorDto>> GetActorById(long id)
        {
            return _actorServices.GetActorById(id);
        }

        [HttpPost]
        public Task<ActionResult> createActor([FromForm] AddActorDto addActorDto)
        {
            return _actorServices.CreateActor(addActorDto);
        }

        [HttpPut("{id:long}")]
        public Task<ActionResult> UpdateActor(long id, [FromForm] UpdateActorDto updateActorDto)
        {
            return _actorServices.UpdateActor(id, updateActorDto);
        }

        [HttpDelete("{id:long}")]
        public Task<ActionResult> RemoveActor(long id)
        {
            return _actorServices.RemoveActor(id);
        }

        [HttpPatch("{id:long}")]
        public Task<ActionResult> PatchActor(long id, [FromBody] JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            return _actorServices.PatchActor(id, patchDocument, ModelState);
        }
    }
}