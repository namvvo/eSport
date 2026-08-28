using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSport.Catalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CurrentRound_End",
                table: "Category_SSM_Mapping",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "CurrentRound_Round",
                table: "Category_SSM_Mapping",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CurrentRound_Start",
                table: "Category_SSM_Mapping",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "LeagueRound",
                table: "Category_SSM_Mapping",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRound_End",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropColumn(
                name: "CurrentRound_Round",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropColumn(
                name: "CurrentRound_Start",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropColumn(
                name: "LeagueRound",
                table: "Category_SSM_Mapping");
        }
    }
}
