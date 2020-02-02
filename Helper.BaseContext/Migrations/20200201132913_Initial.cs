using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Helper.BaseContext.Migrations
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
                    { new Guid("f74c8b08-e89c-4954-8041-7633f6a1b9a7"), null, null, 0.1f },
                    { new Guid("e82be0a5-f511-4718-9a0b-0622a5ad25df"), null, null, 0.2f },
                    { new Guid("f23e73e5-aace-446b-bca2-8aa403af1f13"), null, null, 0.3f },
                    { new Guid("d67b7936-c372-49a2-867e-e662777d1ecf"), null, null, 0.4f },
                    { new Guid("eceefb85-e135-4f23-8e3c-6d67dd74ac32"), null, null, 0.5f }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedDate", "Description", "Image", "Name", "Price", "StarRatingId" },
                values: new object[,]
                {
                    { new Guid("975190e4-cdad-44f5-b2bd-557e7c05fb85"), "GDN-0011", new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leaf rake with 48-inch wooden handle.", "assets/images/leaf_rake.png", "Leaf Rake", 19.95m, new Guid("f74c8b08-e89c-4954-8041-7633f6a1b9a7") },
                    { new Guid("4674bb08-0c16-4a8d-b68d-fd3376004fb2"), "GDN-0023", new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "15 gallon capacity rolling garden cart", "assets/images/garden_cart.png", "Garden Cart", 32.99m, new Guid("e82be0a5-f511-4718-9a0b-0622a5ad25df") },
                    { new Guid("80a533a5-ab59-4a6f-8583-77b26c6914c1"), "TBX-0048", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curved claw steel hammer", "assets/images/hammer.png", "Hammer", 8.9m, new Guid("f23e73e5-aace-446b-bca2-8aa403af1f13") },
                    { new Guid("0279f6c3-369b-4c2d-a752-86f87771e7a3"), "TBX-0022", new DateTime(2019, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "15-inch steel blade hand saw", "assets/images/saw.png", "Saw", 11.55m, new Guid("d67b7936-c372-49a2-867e-e662777d1ecf") },
                    { new Guid("55921f0a-b4e7-4268-a705-4ef5a0e74f24"), "GMG-0042", new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard two-button video game controller", "assets/images/xbox-controller.png", "Video Game Controller", 35.95m, new Guid("eceefb85-e135-4f23-8e3c-6d67dd74ac32") }
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
