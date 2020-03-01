using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreeHandApi.Migrations
{
    public partial class testFirstModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "free_hand");

            migrationBuilder.CreateTable(
                name: "first_model",
                schema: "free_hand",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_first_model", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "first_model",
                schema: "free_hand");
        }
    }
}
