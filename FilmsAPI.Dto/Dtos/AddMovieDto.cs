using FilmsAPI.Dto.Constants;
using FilmsAPI.Dto.Validations;
using FilmsAPI.Dto.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Dto
{
    public class AddMovieDto : MoviePatchDto
    {
        [FileSizeValidator(5)]
        [FileTypeValidator(FileTypes.Image)]
        public IFormFile Poster { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<AddActorMovieDto>>))]
        public List<AddActorMovieDto> Actors { get; set; }
    }
}