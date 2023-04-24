using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMusicApp.Migrations
{
    public partial class fieldsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Album",
                table: "userAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "userAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "userAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "userAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviewUrl",
                table: "userAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Album",
                table: "userAlbum");

            migrationBuilder.DropColumn(
                name: "Artist",
                table: "userAlbum");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "userAlbum");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "userAlbum");

            migrationBuilder.DropColumn(
                name: "PreviewUrl",
                table: "userAlbum");
        }
    }
}
