using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace FilmsAPI.Dao.Migrations
{
    public partial class AddLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Cinema",
                type: "geometry",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Actors",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Birthdate", "Name", "Photo" },
                values: new object[,]
                {
                    { 5L, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Jim Carrey", null },
                    { 6L, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Robert Downey Jr.", null },
                    { 7L, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Chris Evans", null }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4L, "Adventure" },
                    { 5L, "Animation" },
                    { 6L, "Suspense" },
                    { 7L, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "IsCinema", "Poster", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 2L, true, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Avengers: Endgame" },
                    { 3L, false, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Avengers: Infinity Wars" },
                    { 4L, false, null, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Sonic the Hedgehog" },
                    { 5L, false, null, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Emma" },
                    { 6L, false, null, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Wonder Woman 1984" }
                });

            migrationBuilder.InsertData(
                table: "MoviesActors",
                columns: new[] { "ActorId", "MovieId", "Character", "Orden" },
                values: new object[,]
                {
                    { 5L, 4L, "Dr. Ivo Robotnik", 1 },
                    { 6L, 2L, "Tony Stark", 1 },
                    { 6L, 3L, "Tony Stark", 1 },
                    { 7L, 2L, "Steve Rogers", 2 },
                    { 7L, 3L, "Steve Rogers", 2 }
                });

            migrationBuilder.InsertData(
                table: "MoviesGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 4L, 2L },
                    { 4L, 3L },
                    { 4L, 4L },
                    { 4L, 6L },
                    { 6L, 2L },
                    { 6L, 3L },
                    { 6L, 5L },
                    { 6L, 6L },
                    { 7L, 5L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 5L, 4L });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 6L, 2L });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 6L, 3L });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 7L, 2L });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 7L, 3L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 4L, 2L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 4L, 3L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 4L, 4L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 4L, 6L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 6L, 2L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 6L, 3L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 6L, 5L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 6L, 6L });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 7L, 5L });

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cinema");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Actors",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
