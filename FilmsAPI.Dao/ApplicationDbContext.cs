using FilmsAPI.Dao.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace FilmsAPI.Dao
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<MoviesGenres>()
                .HasKey(x => new { x.GenreId, x.MovieId });

            modelBuilder.Entity<MoviesCinema>()
                .HasKey(x => new { x.MovieId, x.CinemaId });

            JunkData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesGenres> MoviesGenres { get; set; }
        public DbSet<MoviesCinema> MoviesCinema { get; set; }

        private void JunkData(ModelBuilder modelBuilder)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 43226);

            modelBuilder.Entity<Cinema>()
                .HasData(new List<Cinema>
                {
                    new Cinema { Id = 1, Name = "Cine Normandie", Location = geometryFactory.CreatePoint(new Coordinate(-70.65187693992922, -33.44659291059971)) },
                    new Cinema { Id = 2, Name = "Cinépolis Estación Central", Location = geometryFactory.CreatePoint(new Coordinate(-70.67814409727907, -33.45148636229063)) },
                    new Cinema { Id = 3, Name = "Cineplanet", Location = geometryFactory.CreatePoint(new Coordinate(-70.60807915444221, -33.507899456976844)) },
                    new Cinema { Id = 4, Name = "Grauman's Chinese Theatre", Location = geometryFactory.CreatePoint(new Coordinate(-118.3409283084818, 34.10226286913486)) },
                });

            var adventure = new Genre() { Id = 4, Name = "Adventure" };
            var animation = new Genre() { Id = 5, Name = "Animation" };
            var suspense = new Genre() { Id = 6, Name = "Suspense" };
            var romance = new Genre() { Id = 7, Name = "Romance" };

            modelBuilder.Entity<Genre>()
                .HasData(new List<Genre>
                {
                    adventure, animation, suspense, romance
                });

            var jimCarreyBirthdate = new DateTime(1962, 01, 17, 0, 0, 0, DateTimeKind.Utc);
            var robertDowneyBirthdate = new DateTime(1965, 4, 4, 0, 0, 0, DateTimeKind.Utc);
            var chrisEvansBirthdate = new DateTime(1981, 06, 13, 0, 0, 0, DateTimeKind.Utc);

            var jimCarrey = new Actor() { Id = 5, Name = "Jim Carrey", Birthdate = jimCarreyBirthdate };
            var robertDowney = new Actor() { Id = 6, Name = "Robert Downey Jr.", Birthdate = robertDowneyBirthdate };
            var chrisEvans = new Actor() { Id = 7, Name = "Chris Evans", Birthdate = chrisEvansBirthdate };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                    jimCarrey, robertDowney, chrisEvans
                });

            var endgameReleaseDate = new DateTime(2019, 04, 26, 0, 0, 0, DateTimeKind.Utc);

            var endgame = new Movie()
            {
                Id = 2,
                Title = "Avengers: Endgame",
                IsCinema = true,
                ReleaseDate = endgameReleaseDate
            };

            var iwReleaseDate = new DateTime(2019, 04, 26, 0, 0, 0, DateTimeKind.Utc);

            var iw = new Movie()
            {
                Id = 3,
                Title = "Avengers: Infinity Wars",
                IsCinema = false,
                ReleaseDate = iwReleaseDate
            };

            var sonicReleaseDate = new DateTime(2020, 02, 28, 0, 0, 0, DateTimeKind.Utc);

            var sonic = new Movie()
            {
                Id = 4,
                Title = "Sonic the Hedgehog",
                IsCinema = false,
                ReleaseDate = sonicReleaseDate
            };

            var emmaReleaseDate = new DateTime(2020, 02, 21, 0, 0, 0, DateTimeKind.Utc);

            var emma = new Movie()
            {
                Id = 5,
                Title = "Emma",
                IsCinema = false,
                ReleaseDate = emmaReleaseDate
            };

            var wonderwomanReleaseDate = new DateTime(2020, 08, 14, 0, 0, 0, DateTimeKind.Utc);

            var wonderwoman = new Movie()
            {
                Id = 6,
                Title = "Wonder Woman 1984",
                IsCinema = false,
                ReleaseDate = wonderwomanReleaseDate
            };

            modelBuilder.Entity<Movie>()
                .HasData(new List<Movie>
                {
                    endgame, iw, sonic, emma, wonderwoman
                });

            modelBuilder.Entity<MoviesGenres>().HasData(
                new List<MoviesGenres>()
                {
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = endgame.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = iw.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = sonic.Id, GenreId = adventure.Id},
                    new MoviesGenres(){MovieId = emma.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = emma.Id, GenreId = romance.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = suspense.Id},
                    new MoviesGenres(){MovieId = wonderwoman.Id, GenreId = adventure.Id},
                });

            modelBuilder.Entity<MoviesActors>().HasData(
                new List<MoviesActors>()
                {
                    new MoviesActors(){MovieId = endgame.Id, ActorId = robertDowney.Id, Character = "Tony Stark", Orden = 1},
                    new MoviesActors(){MovieId = endgame.Id, ActorId = chrisEvans.Id, Character = "Steve Rogers", Orden = 2},
                    new MoviesActors(){MovieId = iw.Id, ActorId = robertDowney.Id, Character = "Tony Stark", Orden = 1},
                    new MoviesActors(){MovieId = iw.Id, ActorId = chrisEvans.Id, Character = "Steve Rogers", Orden = 2},
                    new MoviesActors(){MovieId = sonic.Id, ActorId = jimCarrey.Id, Character = "Dr. Ivo Robotnik", Orden = 1}
                });
        }
    }
}
