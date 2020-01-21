using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poc.DapperWithEF.Migrations
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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "StarRating",
                columns: new[] { "Id", "Description", "Image", "Star" },
                values: new object[,]
                {
                    { new Guid("d69cdd54-4084-4df3-a58d-38c85801e513"), null, null, 3.2f },
                    { new Guid("86134972-7a98-409f-8937-c1ce835361c8"), null, null, 4.2f },
                    { new Guid("d9496d92-4cb9-494f-9843-55f324585b5b"), null, null, 4.8f },
                    { new Guid("eee2517c-8585-42ac-b910-b13928dd2557"), null, null, 3.7f },
                    { new Guid("de203a87-cdef-4865-a6db-49db78490bfd"), null, null, 4.6f }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedDate", "Description", "Image", "Name", "Price", "StarRatingId" },
                values: new object[,]
                {
                    { new Guid("e67f3ca8-ca1c-457b-9fe0-e24d0d71c2b8"), "GDN-0011", new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leaf rake with 48-inch wooden handle.", "assets/images/leaf_rake.png", "Leaf Rake", 19.95m, new Guid("d69cdd54-4084-4df3-a58d-38c85801e513") },
                    { new Guid("8bda45e5-bb56-4a1b-b5e4-0ff2fa6eeebe"), "GDN-0023", new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "15 gallon capacity rolling garden cart", "assets/images/garden_cart.png", "Garden Cart", 32.99m, new Guid("86134972-7a98-409f-8937-c1ce835361c8") },
                    { new Guid("5215d042-67fb-43a5-b9b0-04e18d4f627e"), "TBX-0048", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curved claw steel hammer", "assets/images/hammer.png", "Hammer", 8.9m, new Guid("d9496d92-4cb9-494f-9843-55f324585b5b") },
                    { new Guid("5d674234-5385-41c4-adf9-951ae1e5346a"), "TBX-0022", new DateTime(2019, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "15-inch steel blade hand saw", "assets/images/saw.png", "Saw", 11.55m, new Guid("eee2517c-8585-42ac-b910-b13928dd2557") },
                    { new Guid("d5ddee7c-84cc-4383-9a8a-26eb466056a9"), "GMG-0042", new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard two-button video game controller", "assets/images/xbox-controller.png", "Video Game Controller", 35.95m, new Guid("de203a87-cdef-4865-a6db-49db78490bfd") }
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
