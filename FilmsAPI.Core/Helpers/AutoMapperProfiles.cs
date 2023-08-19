using AutoMapper;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using Microsoft.Extensions.Options;

namespace FilmsAPI.Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<AddGenreDto, Genre>();
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<AddActorDto, Actor>();
            CreateMap<UpdateActorDto, Actor>()
                .ForMember(member => member.Photo, options => options.Ignore());
        }
    }
}