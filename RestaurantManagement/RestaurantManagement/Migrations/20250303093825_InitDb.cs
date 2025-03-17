using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActiveID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    FoodCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.FoodCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodID);
                    table.ForeignKey(
                        name: "FK_Foods_FoodCategories_FoodCategoryID",
                        column: x => x.FoodCategoryID,
                        principalTable: "FoodCategories",
                        principalColumn: "FoodCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_FoodOrders_Customers_UserID",
                        column: x => x.UserID,
                        principalTable: "Customers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodOrders_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FoodID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_CartItems_Customers_UserID",
                        column: x => x.UserID,
                        principalTable: "Customers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Foods_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foods",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodFavorites",
                columns: table => new
                {
                    FoodFavoriteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodFavorites", x => x.FoodFavoriteID);
                    table.ForeignKey(
                        name: "FK_FoodFavorites_Customers_UserID",
                        column: x => x.UserID,
                        principalTable: "Customers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodFavorites_Foods_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foods",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodFeedbacks",
                columns: table => new
                {
                    FoodFeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RatingPoint = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodFeedbacks", x => x.FoodFeedbackID);
                    table.ForeignKey(
                        name: "FK_FoodFeedbacks_Customers_UserID",
                        column: x => x.UserID,
                        principalTable: "Customers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodFeedbacks_Foods_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foods",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodImages",
                columns: table => new
                {
                    FoodID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodImages", x => new { x.FoodID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_FoodImages_Foods_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foods",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    FoodID = table.Column<int>(type: "int", nullable: false),
                    PurchaseQuantity = table.Column<int>(type: "int", nullable: false),
                    IsFeedBack = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_FoodOrderDetails_FoodOrders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "FoodOrders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodOrderDetails_Foods_FoodID",
                        column: x => x.FoodID,
                        principalTable: "Foods",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_FoodID",
                table: "CartItems",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserID",
                table: "CartItems",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodFavorites_FoodID",
                table: "FoodFavorites",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodFavorites_UserID",
                table: "FoodFavorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodFeedbacks_FoodID",
                table: "FoodFeedbacks",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodFeedbacks_UserID",
                table: "FoodFeedbacks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrderDetails_FoodID",
                table: "FoodOrderDetails",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrderDetails_OrderID",
                table: "FoodOrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrders_PaymentMethodID",
                table: "FoodOrders",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrders_UserID",
                table: "FoodOrders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodCategoryID",
                table: "Foods",
                column: "FoodCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "FoodFavorites");

            migrationBuilder.DropTable(
                name: "FoodFeedbacks");

            migrationBuilder.DropTable(
                name: "FoodImages");

            migrationBuilder.DropTable(
                name: "FoodOrderDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "FoodOrders");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "FoodCategories");
        }
    }
}
