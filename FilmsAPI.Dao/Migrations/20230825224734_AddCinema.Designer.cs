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
    [Migration("20230825224734_AddCinema")]
    partial class AddCinema
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
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Cinema", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.ToTable("Cinema");
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
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.MoviesCinema", b =>
                {
                    b.Property<long>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<long>("CinemaId")
                        .HasColumnType("bigint");

                    b.HasKey("MovieId", "CinemaId");

                    b.HasIndex("CinemaId");

                    b.ToTable("MoviesCinema");
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

            modelBuilder.Entity("FilmsAPI.Dao.Entities.MoviesCinema", b =>
                {
                    b.HasOne("FilmsAPI.Dao.Entities.Cinema", "Cinema")
                        .WithMany("MoviesCinema")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmsAPI.Dao.Entities.Movie", "Movie")
                        .WithMany("MoviesCinema")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

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

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Cinema", b =>
                {
                    b.Navigation("MoviesCinema");
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Genre", b =>
                {
                    b.Navigation("MoviesGenres");
                });

            modelBuilder.Entity("FilmsAPI.Dao.Entities.Movie", b =>
                {
                    b.Navigation("MoviesActors");

                    b.Navigation("MoviesCinema");

                    b.Navigation("MoviesGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
