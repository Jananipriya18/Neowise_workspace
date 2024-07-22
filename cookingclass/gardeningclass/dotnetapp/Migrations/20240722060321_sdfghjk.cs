using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class sdfghjk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricalTours",
                columns: table => new
                {
                    HistoricalTourID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalTours", x => x.HistoricalTourID);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HistoricalTourID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantID);
                    table.ForeignKey(
                        name: "FK_Participants_HistoricalTours_HistoricalTourID",
                        column: x => x.HistoricalTourID,
                        principalTable: "HistoricalTours",
                        principalColumn: "HistoricalTourID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HistoricalTours",
                columns: new[] { "HistoricalTourID", "Capacity", "Description", "EndTime", "Location", "StartTime", "TourName" },
                values: new object[,]
                {
                    { 1, 20, "Explore the ancient ruins of Rome including the Colosseum, Roman Forum, and Palatine Hill.", "2023-01-01 12:00 PM", "Rome, Italy", "2023-01-01 10:00 AM", "Ancient Rome Tour" },
                    { 2, 15, "Discover the mysteries of the Great Pyramids and the Sphinx.", "2023-01-11 01:00 PM", "Giza, Egypt", "2023-01-11 10:00 AM", "Egyptian Pyramids Adventure" },
                    { 3, 25, "Visit some of England's most iconic medieval castles and learn about their history.", "2023-01-21 12:00 PM", "Various Locations, England", "2023-01-21 10:00 AM", "Medieval Castles Tour" },
                    { 4, 20, "Explore the art and architecture of Renaissance Florence, including the Duomo and Uffizi Gallery.", "2023-01-31 12:00 PM", "Florence, Italy", "2023-01-31 10:00 AM", "Renaissance Florence Tour" },
                    { 5, 30, "Learn about the events leading up to the American Revolution, including visits to historic sites like the Boston Tea Party Ships and Museum.", "2023-02-10 12:00 PM", "Boston, USA", "2023-02-10 10:00 AM", "American Revolution Tour" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_HistoricalTourID",
                table: "Participants",
                column: "HistoricalTourID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "HistoricalTours");
        }
    }
}
