using AutoMapper;
using FilmsAPI.Dao.Entities;
using FilmsAPI.Dto;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace FilmsAPI.Core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
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
            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(member => member.Genres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(member => member.Actors, options => options.MapFrom(MapMoviesActors));

            CreateMap<CinemaDto, Cinema>()
                .ForMember(member => member.Location, x => x.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));
            CreateMap<AddCinemaDto, Cinema>()
                .ForMember(member => member.Location, x => x.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude)))); ;
            CreateMap<UpdateCinemaDto, Cinema>()
                .ForMember(member => member.Location, x => x.MapFrom(y =>
                    geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude)))); ;
            CreateMap<Cinema, CinemaDto>()
                .ForMember(member => member.Latitude, x => x.MapFrom(y => y.Location.Y))
                .ForMember(member => member.Longitude, x => x.MapFrom(y => y.Location.X));
        }

        private List<ActorMovieDetailDto> MapMoviesActors(Movie movie, MovieDetailsDto movieDetailsDto)
        {
            var result = new List<ActorMovieDetailDto>();
            if (movie.MoviesActors == null)
                return result;
            foreach (var actorMovie in movie.MoviesActors)
                result.Add(new ActorMovieDetailDto()
                {
                    ActorId = actorMovie.ActorId,
                    Character = actorMovie.Character,
                    NamePerson = actorMovie.Actor.Name
                });
            return result;
        }

        private List<GenreDto> MapMoviesGenres(Movie movie, MovieDetailsDto movieDetailsDto)
        {
            var result = new List<GenreDto>();
            if (movie.MoviesGenres == null)
                return result;
            foreach (var genreMovie in movie.MoviesGenres)
                result.Add(new GenreDto() { Id = genreMovie.GenreId, Name = genreMovie.Genre.Name });
            return result;
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