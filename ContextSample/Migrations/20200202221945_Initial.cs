using System;
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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "StarRating",
                columns: new[] { "Id", "Description", "Image", "Star" },
                values: new object[,]
                {
                    { new Guid("d37185b9-b403-47e5-8f83-e5b0523d5276"), null, null, 0.1f },
                    { new Guid("ebff7837-b8e3-4112-9a6b-3b0e6e6257ad"), null, null, 0.2f },
                    { new Guid("99394cf3-40fe-4ec7-9ece-55dbbd32db55"), null, null, 0.3f },
                    { new Guid("e0eb5e32-2fe4-4183-a0d3-dd79945c266a"), null, null, 0.4f },
                    { new Guid("8f72bd3c-feda-47e5-9c3d-e6d2fa9b8f20"), null, null, 0.5f }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedDate", "Description", "Image", "Name", "Price", "StarRatingId" },
                values: new object[,]
                {
                    { new Guid("e1fc2d4c-24b1-48e8-bb2b-ecc305038420"), "GDN-0011", new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leaf rake with 48-inch wooden handle.", "assets/images/leaf_rake.png", "Leaf Rake", 19.95m, new Guid("d37185b9-b403-47e5-8f83-e5b0523d5276") },
                    { new Guid("70e66e29-9ba7-494d-b219-d2e47e32cc55"), "GDN-0023", new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "15 gallon capacity rolling garden cart", "assets/images/garden_cart.png", "Garden Cart", 32.99m, new Guid("ebff7837-b8e3-4112-9a6b-3b0e6e6257ad") },
                    { new Guid("7a89fc1f-9b4a-430b-ad52-2aab847fab58"), "TBX-0048", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curved claw steel hammer", "assets/images/hammer.png", "Hammer", 8.9m, new Guid("99394cf3-40fe-4ec7-9ece-55dbbd32db55") },
                    { new Guid("cf9ae9f4-8de0-4d07-8e67-c53c23a8b29d"), "TBX-0022", new DateTime(2019, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "15-inch steel blade hand saw", "assets/images/saw.png", "Saw", 11.55m, new Guid("e0eb5e32-2fe4-4183-a0d3-dd79945c266a") },
                    { new Guid("eeefd27a-3f6b-45f6-8df7-83de8647e53e"), "GMG-0042", new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard two-button video game controller", "assets/images/xbox-controller.png", "Video Game Controller", 35.95m, new Guid("8f72bd3c-feda-47e5-9c3d-e6d2fa9b8f20") }
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
