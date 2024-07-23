using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class sdfghjkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VRExperiences",
                columns: table => new
                {
                    VRExperienceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VRExperiences", x => x.VRExperienceID);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    AttendeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VRExperienceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.AttendeeID);
                    table.ForeignKey(
                        name: "FK_Attendees_VRExperiences_VRExperienceID",
                        column: x => x.VRExperienceID,
                        principalTable: "VRExperiences",
                        principalColumn: "VRExperienceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VRExperiences",
                columns: new[] { "VRExperienceID", "Description", "EndTime", "ExperienceName", "Location", "MaxCapacity", "StartTime" },
                values: new object[,]
                {
                    { 1, "Explore the wonders of space in a fully immersive virtual reality experience.", "2023-01-01T12:00:00", "Virtual Space Exploration", "Virtual", 10, "2023-01-01T10:00:00" },
                    { 2, "Take a tour through history with this engaging VR experience.", "2023-01-11T13:00:00", "Historical VR Tour", "Virtual", 10, "2023-01-11T10:00:00" },
                    { 3, "Dive into the depths of the ocean and explore underwater life like never before.", "2023-02-01T16:00:00", "Underwater Adventure", "Virtual", 10, "2023-02-01T14:00:00" },
                    { 4, "Experience the thrill of mountain climbing from the safety of your home.", "2023-02-15T11:00:00", "Mountain Climbing Expedition", "Virtual", 10, "2023-02-15T09:00:00" },
                    { 5, "Travel back in time to ancient Rome and witness its grandeur and history.", "2023-03-01T12:00:00", "Ancient Rome Tour", "Virtual", 10, "2023-03-01T10:00:00" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_VRExperienceID",
                table: "Attendees",
                column: "VRExperienceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "VRExperiences");
        }
    }
}
