using FilmsAPI.Core;
using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreServices _genreServices;

        public GenreController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [HttpGet]
        public Task<ActionResult<List<GenreDto>>> GetCollectionGenres()
        {
            return _genreServices.CollectionGenres();
        }

        [HttpGet("{id:long}")]
        public Task<ActionResult<GenreDto>> GetGenreById(long id)
        {
            return _genreServices.GenreById(id);
        }

        [HttpPost]
        public Task<ActionResult> AddGenre([FromBody] AddGenreDto addGenreDto)
        {
            return _genreServices.AddGenre(addGenreDto);
        }

        [HttpPut("{id:long}")]
        public Task<ActionResult> UpdateGenre(long id, [FromBody] UpdateGenreDto updateGenreDto)
        {
            return _genreServices.UpdateGenre(id, updateGenreDto);
        }

        [HttpDelete("{id:long}")]
        public Task<ActionResult> RemoveGenre(long id)
        {
            return _genreServices.RemoveGenre(id);
        }
    }
}