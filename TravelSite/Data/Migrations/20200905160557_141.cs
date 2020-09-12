using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelSite.Data.Migrations
{
    public partial class _141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Blogs");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Blogs",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
