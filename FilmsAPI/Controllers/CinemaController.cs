using FilmsAPI.Core;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("api/cinema")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaServices _cinemaServices;

        public CinemaController(ICinemaServices cinemaServices)
        {
            _cinemaServices = cinemaServices;
        }

        [HttpGet]
        public Task<ActionResult<List<CinemaDto>>> CollectionCinema()
        {
            return _cinemaServices.CollectionCinema();
        }

        [HttpGet("{id:long}")]
        public Task<ActionResult<CinemaDto>> CinemaById(long id)
        {
            return _cinemaServices.CinemaById(id);
        }

        [HttpGet("{closed}")]
        public Task<ActionResult<List<CinemaCloseDto>>> CollectionCloseCinema([FromQuery] CinemaCinemaFilterDto cinemaCinemaFilterDto)
        {
            return _cinemaServices.CollectionCloseCinema(cinemaCinemaFilterDto);
        }

        [HttpPost]
        public Task<ActionResult> CreateCinema([FromBody] AddCinemaDto addCinemaDto)
        {
            return _cinemaServices.CreateCinema(addCinemaDto);
        }

        [HttpPut("{id:long}")]
        public Task<ActionResult> UpdateCinema(long id, UpdateCinemaDto updateCinemaDto)
        {
            return _cinemaServices.UpdateCinema(id, updateCinemaDto);
        }

        [HttpDelete("{id:long}")]
        public Task<ActionResult> RemoveCinema(long id)
        {
            return _cinemaServices.RemoveCinema(id);
        }
    }
}