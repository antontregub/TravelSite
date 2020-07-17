using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelSite.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Mark = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TravelId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
