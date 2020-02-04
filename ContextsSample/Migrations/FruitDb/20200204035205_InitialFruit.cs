using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextsSample.Migrations.FruitDb
{
    public partial class InitialFruit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "many_contexts");

            migrationBuilder.CreateTable(
                name: "Fruit",
                schema: "many_contexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fruit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fruit",
                schema: "many_contexts");
        }
    }
}
