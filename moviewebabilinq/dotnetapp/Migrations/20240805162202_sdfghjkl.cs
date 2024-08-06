using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class sdfghjkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "LibraryCards");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, "alice.johnson@example.com", "Alice Johnson", "123-456-7890" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[] { 2, "bob.smith@example.com", "Bob Smith", "987-654-3210" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CustomerId",
                table: "Movies",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.CreateTable(
                name: "LibraryCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryCardId = table.Column<int>(type: "int", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PublishedYear = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_LibraryCards_LibraryCardId",
                        column: x => x.LibraryCardId,
                        principalTable: "LibraryCards",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "LibraryCards",
                columns: new[] { "Id", "CardNumber", "ExpiryDate", "MemberName" },
                values: new object[] { 1, "LC-12345", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe" });

            migrationBuilder.InsertData(
                table: "LibraryCards",
                columns: new[] { "Id", "CardNumber", "ExpiryDate", "MemberName" },
                values: new object[] { 2, "LC-54321", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryCardId",
                table: "Books",
                column: "LibraryCardId");
        }
    }
}
