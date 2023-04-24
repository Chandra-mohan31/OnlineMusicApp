using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMusicApp.Migrations
{
    public partial class track : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackId",
                table: "userAlbum",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "userAlbum");
        }
    }
}
