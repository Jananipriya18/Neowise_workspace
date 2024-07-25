using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class asdfghjk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    playlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playlistName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    songName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    yearOfRelease = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    artistName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    genre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.playlistId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
