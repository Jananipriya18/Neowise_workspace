using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class sdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheeseShops",
                columns: table => new
                {
                    shopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ownerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cheeseSpecialties = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    experienceYears = table.Column<int>(type: "int", nullable: false),
                    storeLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    importedCountry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheeseShops", x => x.shopId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheeseShops");
        }
    }
}
