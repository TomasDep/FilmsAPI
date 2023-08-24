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
            CreateMap<UpdateGenreDto, Genre>();

            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<AddActorDto, Actor>();
            CreateMap<UpdateActorDto, Actor>()
                .ForMember(member => member.Photo, options => options.Ignore());
            CreateMap<ActorPatchDto, Actor>().ReverseMap();

            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<AddMovieDto, Movie>()
                .ForMember(member => member.Poster, options => options.Ignore())
                .ForMember(member => member.MoviesGenres, options => options.MapFrom(MapAddMoviesGenres))
                .ForMember(member => member.MoviesActors, options => options.MapFrom(MapAddMoviesActors));
            CreateMap<UpdateMovieDto, Movie>()
                .ForMember(member => member.Poster, options => options.Ignore())
                .ForMember(member => member.MoviesGenres, options => options.MapFrom(MapUpdateMoviesGenres))
                .ForMember(member => member.MoviesActors, options => options.MapFrom(MapUpdateMoviesActors));
            CreateMap<MoviePatchDto, Movie>().ReverseMap();
        }

        private List<MoviesGenres> MapAddMoviesGenres(AddMovieDto addMovieDto, Movie movie)
        {
            var result = new List<MoviesGenres>();
            if (addMovieDto.GenresIds == null)
                return result;
            foreach (var id in addMovieDto.GenresIds)
                result.Add(new MoviesGenres() { GenreId = id });
            return result;
        }

        private List<MoviesActors> MapAddMoviesActors(AddMovieDto addMovieDto, Movie movie)
        {
            var result = new List<MoviesActors>();
            if (addMovieDto.Actors == null)
                return result;
            foreach (var actor in addMovieDto.Actors)
                result.Add(new MoviesActors() { ActorId = actor.ActorId, Character = actor.Character });
            return result;
        }

        private List<MoviesGenres> MapUpdateMoviesGenres(UpdateMovieDto updateMovieDto, Movie movie)
        {
            var result = new List<MoviesGenres>();
            if (updateMovieDto.GenresIds == null)
                return result;
            foreach (var id in updateMovieDto.GenresIds)
                result.Add(new MoviesGenres() { GenreId = id });
            return result;
        }

        private List<MoviesActors> MapUpdateMoviesActors(AddMovieDto addMovieDto, Movie movie)
        {
            var result = new List<MoviesActors>();
            if (addMovieDto.Actors == null)
                return result;
            foreach (var actor in addMovieDto.Actors)
                result.Add(new MoviesActors() { ActorId = actor.ActorId, Character = actor.Character });
            return result;
        }
    }
}