using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextsSample.Migrations
{
    public partial class InitialPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "many_contexts");

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "many_contexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Login = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person",
                schema: "many_contexts");
        }
    }
}
