using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Core
{
    public interface IGenreServices
    {
        Task<ActionResult<List<GenreDto>>> CollectionGenres();
        Task<ActionResult<GenreDto>> GenreById(long id);
        Task<ActionResult> AddGenre(AddGenreDto addGenreDto);
        Task<ActionResult> UpdateGenre(long id, UpdateGenreDto updateGenreDto);
        Task<ActionResult> RemoveGenre(long id);
    }
}