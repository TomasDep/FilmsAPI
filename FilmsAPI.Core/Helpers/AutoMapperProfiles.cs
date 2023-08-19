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
            CreateMap<ActorPatchDto, Actor>().ReverseMap();

            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<AddMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(member => member.Poster, options => options.Ignore());
            CreateMap<MoviePatchDto, Movie>().ReverseMap();
        }
    }
}