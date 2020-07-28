using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelSite.Data.Migrations
{
    public partial class post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TravelId",
                table: "Reviews",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TravelId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
