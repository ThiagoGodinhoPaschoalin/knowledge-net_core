using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextSample.Migrations
{
    public partial class AddUpdatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "dbo",
                table: "Products",
                type: "DATETIME2(7)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "Products");
        }
    }
}
