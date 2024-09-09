using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class updateLaptopAndColorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Books_BookId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeRatings_Books_BookId",
                table: "LikeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Books_BookId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Ratings",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_BookId",
                table: "Ratings",
                newName: "IX_Ratings_LaptopId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "OrderDetails",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_BookId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_LaptopId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "LikeRatings",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_LikeRatings_BookId",
                table: "LikeRatings",
                newName: "IX_LikeRatings_LaptopId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "CartItems",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_BookId",
                table: "CartItems",
                newName: "IX_CartItems_LaptopId");

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laptops_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_CategoryId",
                table: "Laptops",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_ColorId",
                table: "Laptops",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Laptops_LaptopId",
                table: "CartItems",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeRatings_Laptops_LaptopId",
                table: "LikeRatings",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Laptops_LaptopId",
                table: "OrderDetails",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Laptops_LaptopId",
                table: "Ratings",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Laptops_LaptopId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeRatings_Laptops_LaptopId",
                table: "LikeRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Laptops_LaptopId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Laptops_LaptopId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "Ratings",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_LaptopId",
                table: "Ratings",
                newName: "IX_Ratings_BookId");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "OrderDetails",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_LaptopId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_BookId");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "LikeRatings",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_LikeRatings_LaptopId",
                table: "LikeRatings",
                newName: "IX_LikeRatings_BookId");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "CartItems",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_LaptopId",
                table: "CartItems",
                newName: "IX_CartItems_BookId");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HomeTown = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Story = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    YearOfBirth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AverageStars = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Books_BookId",
                table: "CartItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeRatings_Books_BookId",
                table: "LikeRatings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Books_BookId",
                table: "OrderDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
