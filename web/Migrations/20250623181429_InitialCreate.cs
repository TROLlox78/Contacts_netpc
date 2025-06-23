using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryDict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDict", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryDict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryDict", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CategoryDict",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "służbowy" },
                    { 2, "prywatny" },
                    { 3, "inny" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CategoryId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "SubCategory" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2002, 2, 20), "jkowlaski@mail.com", "Jan", "Kowalski", "b91f82cfd4e5a2201290f6de6c4b73322d03abdb", "123123123", "szef" },
                    { 2, 2, new DateOnly(2003, 3, 20), "apanna@mail.com", "Anna", "Panna", "3572112c8b9ab0de9d437f9a2fc24999ffbd8c56", "98712412", "" },
                    { 3, 3, new DateOnly(1999, 1, 1), "kkowlaski@mail.com", "Krzysztof", "Kowalski", "190af9a37aed7604bb36b7c831835961e18dd19c", "098123456", "dowolne" }
                });

            migrationBuilder.InsertData(
                table: "SubCategoryDict",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "szef" },
                    { 2, "klient" },
                    { 3, "stazysta" },
                    { 4, "pracownik" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDict");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "SubCategoryDict");
        }
    }
}
