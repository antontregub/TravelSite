using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelSite.Data.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TripId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TripId",
                table: "Reviews",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Trips_TripId",
                table: "Reviews",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Trips_TripId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TripId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Reviews");
        }
    }
}
