using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelSite.Data.Migrations
{
    public partial class lang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Blogs_BlogId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BlogId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "BlogLanguage",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Blogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                columns: new[] { "Id", "Language" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BlogId_BlogLanguage",
                table: "Reviews",
                columns: new[] { "BlogId", "BlogLanguage" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Blogs_BlogId_BlogLanguage",
                table: "Reviews",
                columns: new[] { "BlogId", "BlogLanguage" },
                principalTable: "Blogs",
                principalColumns: new[] { "Id", "Language" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Blogs_BlogId_BlogLanguage",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BlogId_BlogLanguage",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogLanguage",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Blogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BlogId",
                table: "Reviews",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Blogs_BlogId",
                table: "Reviews",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
