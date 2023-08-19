using System.ComponentModel.DataAnnotations;
using FilmsAPI.Dto.Constants;
using FilmsAPI.Dto.Validations;
using Microsoft.AspNetCore.Http;

namespace FilmsAPI.Dto
{
    public class UpdateActorDto : ActorPatchDto
    {
        [FileSizeValidator(5)]
        [FileTypeValidator(FileTypes.Image)]
        public IFormFile Photo { get; set; }
    }
}