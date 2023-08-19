using FilmsAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FilmsAPI.Core
{
    public interface IMovieServices
    {
        Task<ActionResult<List<MovieDto>>> CollectionMovies();
        Task<ActionResult<List<MovieDto>>> CollectionMoviesPaginate(PaginationDto paginationDto, HttpContext httpContext);
        Task<ActionResult<MovieDto>> GetMovieById(long id);
        Task<ActionResult> CreateMovie(AddMovieDto addMovieDto);
        Task<ActionResult> UpdateMovie(long id, UpdateMovieDto updateMovieDto);
        Task<ActionResult> PatchMovie(long id, JsonPatchDocument<MoviePatchDto> patchDocument, ModelStateDictionary modelState);
        Task<ActionResult> RemoveMovie(long id);
    }
}