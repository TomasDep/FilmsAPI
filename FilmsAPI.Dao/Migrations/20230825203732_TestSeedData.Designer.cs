﻿// <auto-generated />
using System;
using FilmsAPI.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FilmsAPI.Dao.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230825203732_TestSeedData")]
    partial class TestSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Actor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 5L,
                            Birthdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jim Carrey"
                        },
                        new
                        {
                            Id = 6L,
                            Birthdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 7L,
                            Birthdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Chris Evans"
                        });
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 4L,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "Suspense"
                        },
                        new
                        {
                            Id = 7L,
                            Name = "Romance"
                        });
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsCinema")
                        .HasColumnType("boolean");

                    b.Property<string>("Poster")
                        .HasColumnType("text");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            IsCinema = true,
                            ReleaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 3L,
                            IsCinema = false,
                            ReleaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 4L,
                            IsCinema = false,
                            ReleaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 5L,
                            IsCinema = false,
                            ReleaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Emma"
                        },
                        new
                        {
                            Id = 6L,
                            IsCinema = false,
                            ReleaseDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Wonder Woman 1984"
                        });
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.MoviesActors", b =>
                {
                    b.Property<long>("ActorId")
                        .HasColumnType("bigint");

                    b.Property<long>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Character")
                        .HasColumnType("text");

                    b.Property<int>("Orden")
                        .HasColumnType("integer");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesActors");

                    b.HasData(
                        new
                        {
                            ActorId = 6L,
                            MovieId = 2L,
                            Character = "Tony Stark",
                            Orden = 1
                        },
                        new
                        {
                            ActorId = 7L,
                            MovieId = 2L,
                            Character = "Steve Rogers",
                            Orden = 2
                        },
                        new
                        {
                            ActorId = 6L,
                            MovieId = 3L,
                            Character = "Tony Stark",
                            Orden = 1
                        },
                        new
                        {
                            ActorId = 7L,
                            MovieId = 3L,
                            Character = "Steve Rogers",
                            Orden = 2
                        },
                        new
                        {
                            ActorId = 5L,
                            MovieId = 4L,
                            Character = "Dr. Ivo Robotnik",
                            Orden = 1
                        });
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.MoviesGenres", b =>
                {
                    b.Property<long>("GenreId")
                        .HasColumnType("bigint");

                    b.Property<long>("MovieId")
                        .HasColumnType("bigint");

                    b.HasKey("GenreId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesGenres");

                    b.HasData(
                        new
                        {
                            GenreId = 6L,
                            MovieId = 2L
                        },
                        new
                        {
                            GenreId = 4L,
                            MovieId = 2L
                        },
                        new
                        {
                            GenreId = 6L,
                            MovieId = 3L
                        },
                        new
                        {
                            GenreId = 4L,
                            MovieId = 3L
                        },
                        new
                        {
                            GenreId = 4L,
                            MovieId = 4L
                        },
                        new
                        {
                            GenreId = 6L,
                            MovieId = 5L
                        },
                        new
                        {
                            GenreId = 7L,
                            MovieId = 5L
                        },
                        new
                        {
                            GenreId = 6L,
                            MovieId = 6L
                        },
                        new
                        {
                            GenreId = 4L,
                            MovieId = 6L
                        });
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.MoviesActors", b =>
                {
                    b.HasOne("FilmsAPI.Dao.Entities.Actor", "Actor")
                        .WithMany("MoviesActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmsAPI.Dao.Entities.Movie", "Movie")
                        .WithMany("MoviesActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.MoviesGenres", b =>
                {
                    b.HasOne("FilmsAPI.Dao.Entities.Genre", "Genre")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmsAPI.Dao.Entities.Movie", "Movie")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Actor", b =>
                {
                    b.Navigation("MoviesActors");
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Genre", b =>
                {
                    b.Navigation("MoviesGenres");
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Movie", b =>
                {
                    b.Navigation("MoviesActors");

                    b.Navigation("MoviesGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
