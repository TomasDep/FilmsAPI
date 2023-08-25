using AutoMapper;
using FilmsAPI.Dao;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using FilmsAPI.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FilmsAPI.Core.Services
{
    public class ActorServicesImpl : IActorServices
    {
        private readonly IActorRepository _actorRepository;
        private readonly IStorageFiles _storageFiles;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ActorServicesImpl> _logger;
        private readonly string _container = "actorsContainer";

        public ActorServicesImpl(
            IActorRepository actorRepository,
            IStorageFiles storageFiles,
            IMapper mapper,
            ApplicationDbContext context,
            ILogger<ActorServicesImpl> logger
        )
        {
            _actorRepository = actorRepository;
            _storageFiles = storageFiles;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult<List<ActorDto>>> CollectionActors()
        {
            try
            {
                var actors = await _actorRepository.CollectionActors();
                var actorsDto = _mapper.Map<List<ActorDto>>(actors);
                return new OkObjectResult(actorsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<List<ActorDto>>> CollectionActorsPaginate(PaginationDto paginationDto, HttpContext httpContext)
        {
            try
            {
                var queryable = _context.Actors.AsQueryable();
                await httpContext.InsertParameterPagination(queryable, paginationDto.NumberRecordsPerPage);
                var actors = await queryable.Paginate(paginationDto).ToListAsync();
                var actorsDto = _mapper.Map<List<ActorDto>>(actors);
                return new OkObjectResult(actorsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<ActorDto>> GetActorById(long id)
        {
            try
            {
                var actor = await _actorRepository.GetActorById(id);
                if (actor == null)
                    return new NotFoundObjectResult($"Actor by id: {id} not exists.");
                var actorDto = _mapper.Map<ActorDto>(actor);
                return new OkObjectResult(actorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> CreateActor(AddActorDto addActorDto)
        {
            try
            {
                var actor = _mapper.Map<Actor>(addActorDto);
                if (addActorDto.Photo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await addActorDto.Photo.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(addActorDto.Photo.FileName);
                        actor.Photo = await _storageFiles.SaveFile(content, extension, _container, addActorDto.Photo.ContentType);
                    }
                }
                await _actorRepository.CreateActor(actor);
                var actorDto = _mapper.Map<ActorDto>(actor);
                return new ObjectResult(actorDto) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> UpdateActor(long id, UpdateActorDto updateActorDto)
        {
            try
            {
                var actor = await _actorRepository.GetActorById(id);
                if (actor == null)
                    return new NotFoundObjectResult($"Actor by id: {id} not exists.");
                actor.Id = id;
                actor.Name = updateActorDto.Name;
                actor.Birthdate = updateActorDto.Birthdate;
                if (updateActorDto.Photo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await updateActorDto.Photo.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(updateActorDto.Photo.FileName);
                        actor.Photo = await _storageFiles.UpdateFile(
                            content,
                            extension,
                            _container,
                            actor.Photo,
                            updateActorDto.Photo.ContentType
                        );
                    }
                }
                await _actorRepository.UpdateActor(actor);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveActor(long id)
        {
            try
            {
                var actor = await _actorRepository.GetActorById(id);
                if (actor == null)
                    return new NotFoundObjectResult($"Actor by id: {id} not exists.");
                if (actor.Photo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await _storageFiles.RemoveFile(actor.Photo, _container);
                    }
                }
                await _actorRepository.RemoveActor(actor);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> PatchActor(long id, JsonPatchDocument<ActorPatchDto> patchDocument, ModelStateDictionary modelState)
        {
            try
            {
                if (patchDocument == null)
                    return new BadRequestObjectResult(patchDocument);
                var actor = await _actorRepository.GetActorById(id);
                if (actor == null)
                    return new NotFoundObjectResult($"Actor by id: {id} not exists.");
                var actorDto = _mapper.Map<ActorPatchDto>(actor);
                patchDocument.ApplyTo(actorDto, modelState);
                _mapper.Map(actorDto, actor);
                await _context.SaveChangesAsync();
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}