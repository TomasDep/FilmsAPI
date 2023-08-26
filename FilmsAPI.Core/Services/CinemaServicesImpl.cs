using AutoMapper;
using FilmsAPI.Dto;
using FilmsAPI.Dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmsAPI.Dao.Entities;
using NetTopologySuite.Geometries;

namespace FilmsAPI.Core.Services
{
    public class CinemaServicesImpl : ICinemaServices
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly GeometryFactory _geometryFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<CinemaServicesImpl> _logger;

        public CinemaServicesImpl(
            ICinemaRepository cinemaRepository,
            GeometryFactory geometryFactory,
            IMapper mapper,
            ILogger<CinemaServicesImpl> logger
        )
        {
            _cinemaRepository = cinemaRepository;
            _geometryFactory = geometryFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActionResult<CinemaDto>> CinemaById(long id)
        {
            try
            {
                var cinema = await _cinemaRepository.GetCinemaById(id);
                if (cinema == null)
                    return new NotFoundObjectResult($"Cinema by id: {id} not exists.");
                var cinemaDto = _mapper.Map<CinemaDto>(cinema);
                return new OkObjectResult(cinemaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<List<CinemaDto>>> CollectionCinema()
        {
            try
            {
                var cinemas = await _cinemaRepository.CollectionCinemas();
                var cinemasDto = _mapper.Map<List<CinemaDto>>(cinemas);
                return new OkObjectResult(cinemasDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<List<CinemaCloseDto>>> CollectionCloseCinema(CinemaCinemaFilterDto cinemaCinemaFilterDto)
        {
            try
            {
                var userLocation = _geometryFactory.CreatePoint(
                    new Coordinate(cinemaCinemaFilterDto.Longitude, cinemaCinemaFilterDto.Latitude)
                );
                var cinemas = await _cinemaRepository.GetCinemasClosed(userLocation, cinemaCinemaFilterDto);
                return new OkObjectResult(cinemas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> CreateCinema(AddCinemaDto addCinemaDto)
        {
            try
            {
                var cinema = _mapper.Map<Cinema>(addCinemaDto);
                await _cinemaRepository.CreateCinema(cinema);
                var cinemaDto = _mapper.Map<CinemaDto>(cinema);
                return new ObjectResult(cinemaDto) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveCinema(long id)
        {
            try
            {
                var cinema = await _cinemaRepository.GetCinemaById(id);
                if (cinema == null)
                    return new NotFoundObjectResult($"Cinema by id: {id} not exists.");
                await _cinemaRepository.RemoveCinema(cinema);
                return new ObjectResult("") { StatusCode = 204 };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Core Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> UpdateCinema(long id, UpdateCinemaDto updateActorDto)
        {
            try
            {
                var cinema = await _cinemaRepository.GetCinemaById(id);
                if (cinema == null)
                    return new NotFoundObjectResult($"Cinema by id: {id} not exists.");
                cinema.Id = id;
                cinema.Name = updateActorDto.Name;
                await _cinemaRepository.UpdateCinema(cinema);
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