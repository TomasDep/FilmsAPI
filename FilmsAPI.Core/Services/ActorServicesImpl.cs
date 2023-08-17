using AutoMapper;
using FilmsAPI.Dao;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilmsAPI.Core.Services
{
    public class ActorServicesImpl : IActorServices
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ActorServicesImpl> _logger;

        public ActorServicesImpl(
            IActorRepository actorRepository,
            IMapper mapper,
            ILogger<ActorServicesImpl> logger
        )
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
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
                _logger.LogInformation($"{ex}");
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
                _logger.LogInformation($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> CreateActor(AddActorDto addActorDto)
        {
            try
            {
                var actor = _mapper.Map<Actor>(addActorDto);
                await _actorRepository.CreateActor(actor);
                var actorDto = _mapper.Map<ActorDto>(actor);
                return new OkObjectResult(actorDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> UpdateActor(long id, UpdateActorDto updateActorDto)
        {
            try
            {
                var actor = _mapper.Map<Actor>(updateActorDto);
                actor.Id = id;
                await _actorRepository.UpdateActor(actor);
                var actorDto = _mapper.Map<ActorDto>(actor);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveActor(long id)
        {
            try
            {
                var isActor = await _actorRepository.IsActorById(id);
                if (!isActor)
                    return new NotFoundObjectResult($"Actor by id: {id} not exists.");
                await _actorRepository.RemoveActorById(id);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}