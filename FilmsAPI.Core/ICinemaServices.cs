using FilmsAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Core
{
    public interface ICinemaServices
    {
        Task<ActionResult<List<CinemaDto>>> CollectionCinema();
        Task<ActionResult<CinemaDto>> CinemaById(long id);
        Task<ActionResult> CreateCinema(AddCinemaDto addCinemaDto);
        Task<ActionResult> UpdateCinema(long id, UpdateCinemaDto updateActorDto);
        Task<ActionResult> RemoveCinema(long id);
    }
}