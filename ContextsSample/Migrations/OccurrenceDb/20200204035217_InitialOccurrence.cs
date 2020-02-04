using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextsSample.Migrations.OccurrenceDb
{
    public partial class InitialOccurrence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "many_contexts");

            migrationBuilder.CreateTable(
                name: "Occurrence",
                schema: "many_contexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    Who = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    What = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occurrence", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occurrence",
                schema: "many_contexts");
        }
    }
}
