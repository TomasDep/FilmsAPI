using AutoMapper;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;

namespace FilmsAPI.Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<AddGenreDto, Genre>();
            CreateMap<Genre, AddGenreDto>();
        }
    }
}