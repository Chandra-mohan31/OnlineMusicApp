using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMusicApp.Migrations
{
    public partial class useralbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userAlbum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusicAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userAlbum_AspNetUsers_MusicAppUserId",
                        column: x => x.MusicAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_userAlbum_MusicAppUserId",
                table: "userAlbum",
                column: "MusicAppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userAlbum");
        }
    }
}
