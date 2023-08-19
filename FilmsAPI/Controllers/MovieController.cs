using FilmsAPI.Core;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieServices _movieService;

        public MovieController(IMovieServices movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public Task<ActionResult<List<MovieDto>>> MovieCollection()
        {
            return _movieService.CollectionMovies();
        }

        [HttpGet("{id:long}")]
        public Task<ActionResult<MovieDto>> GetMovieById(long id)
        {
            return _movieService.GetMovieById(id);
        }

        [HttpPost]
        public Task<ActionResult> CreateMovie([FromForm] AddMovieDto addMovieDto)
        {
            return _movieService.CreateMovie(addMovieDto);
        }

        [HttpPut("{id:long}")]
        public Task<ActionResult> UpdateMovie(long id, [FromForm] UpdateMovieDto updateMovieDto)
        {
            return _movieService.UpdateMovie(id, updateMovieDto);

        }

        [HttpPatch("{id:long}")]
        public Task<ActionResult> PatchMovie(long id, [FromBody] JsonPatchDocument<MoviePatchDto> patchDocument)
        {
            return _movieService.PatchMovie(id, patchDocument, ModelState);
        }

        [HttpDelete("{id:long}")]
        public Task<ActionResult> RemoveMovie(long id)
        {
            return _movieService.RemoveMovie(id);
        }
    }
}