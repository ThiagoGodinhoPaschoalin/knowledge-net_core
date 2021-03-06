﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextSample.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "StarRating",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Star = table.Column<float>(type: "FLOAT(24)", nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarRating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    StarRatingId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_StarRating_StarRatingId",
                        column: x => x.StarRatingId,
                        principalSchema: "dbo",
                        principalTable: "StarRating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_StarRatingId",
                schema: "dbo",
                table: "Products",
                column: "StarRatingId");

            migrationBuilder.CreateIndex(
                name: "UK_StarRating_Star",
                schema: "dbo",
                table: "StarRating",
                column: "Star",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StarRating",
                schema: "dbo");
        }
    }
}
