using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Poc.UOW.Migrations
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
                    { new Guid("1e8cc7c1-28c9-48f6-953e-6355e3434657"), null, null, 0.1f },
                    { new Guid("03b84894-3c8e-4201-b866-70565ba0df9c"), null, null, 0.2f },
                    { new Guid("4a9bb30b-3f65-44b6-8e54-c13aae68fa08"), null, null, 0.3f },
                    { new Guid("c4e7d07f-ff1d-41d5-8498-a2858bc69063"), null, null, 0.4f },
                    { new Guid("f0524334-8d11-4b5e-86fb-a2a0104eea3b"), null, null, 0.5f }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedDate", "Description", "Image", "Name", "Price", "StarRatingId" },
                values: new object[,]
                {
                    { new Guid("8ba9ba47-b00a-4afd-bf47-16a78bdc0caf"), "GDN-0011", new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leaf rake with 48-inch wooden handle.", "assets/images/leaf_rake.png", "Leaf Rake", 19.95m, new Guid("1e8cc7c1-28c9-48f6-953e-6355e3434657") },
                    { new Guid("e4609429-21fa-4251-b312-8ded86141ac0"), "GDN-0023", new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "15 gallon capacity rolling garden cart", "assets/images/garden_cart.png", "Garden Cart", 32.99m, new Guid("03b84894-3c8e-4201-b866-70565ba0df9c") },
                    { new Guid("f7027a6f-a056-4818-b7ad-41636be28454"), "TBX-0048", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curved claw steel hammer", "assets/images/hammer.png", "Hammer", 8.9m, new Guid("4a9bb30b-3f65-44b6-8e54-c13aae68fa08") },
                    { new Guid("4e15bd2e-4772-4695-afb4-874a8168dfaa"), "TBX-0022", new DateTime(2019, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "15-inch steel blade hand saw", "assets/images/saw.png", "Saw", 11.55m, new Guid("c4e7d07f-ff1d-41d5-8498-a2858bc69063") },
                    { new Guid("18e7a708-1d46-4d19-964d-42b5ce4e7deb"), "GMG-0042", new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Standard two-button video game controller", "assets/images/xbox-controller.png", "Video Game Controller", 35.95m, new Guid("f0524334-8d11-4b5e-86fb-a2a0104eea3b") }
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
