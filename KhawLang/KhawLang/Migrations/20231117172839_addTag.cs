using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhawLang.Migrations
{
    /// <inheritdoc />
    public partial class addTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsInput",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "PaymentInfo",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfo", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentItemPaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_PaymentInfo_PaymentItemPaymentId",
                        column: x => x.PaymentItemPaymentId,
                        principalTable: "PaymentInfo",
                        principalColumn: "PaymentId");
                });

            migrationBuilder.CreateTable(
                name: "MealTag",
                columns: table => new
                {
                    MealsMealId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTag", x => new { x.MealsMealId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_MealTag_Meals_MealsMealId",
                        column: x => x.MealsMealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTags",
                columns: table => new
                {
                    MealTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTags", x => x.MealTagId);
                    table.ForeignKey(
                        name: "FK_MealTags_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Cart_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "MealPlan",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlan", x => x.PlanID);
                    table.ForeignKey(
                        name: "FK_MealPlan_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "MealPlanMealTag",
                columns: table => new
                {
                    MealPlansPlanID = table.Column<int>(type: "int", nullable: false),
                    MealTagsMealTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanMealTag", x => new { x.MealPlansPlanID, x.MealTagsMealTagId });
                    table.ForeignKey(
                        name: "FK_MealPlanMealTag_MealPlan_MealPlansPlanID",
                        column: x => x.MealPlansPlanID,
                        principalTable: "MealPlan",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlanMealTag_MealTags_MealTagsMealTagId",
                        column: x => x.MealTagsMealTagId,
                        principalTable: "MealTags",
                        principalColumn: "MealTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerID",
                table: "Cart",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PaymentItemPaymentId",
                table: "Customer",
                column: "PaymentItemPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlan_CustomerID",
                table: "MealPlan",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanMealTag_MealTagsMealTagId",
                table: "MealPlanMealTag",
                column: "MealTagsMealTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTag_TagsTagId",
                table: "MealTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTags_MealId",
                table: "MealTags",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTags_TagId",
                table: "MealTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "MealPlanMealTag");

            migrationBuilder.DropTable(
                name: "MealTag");

            migrationBuilder.DropTable(
                name: "MealPlan");

            migrationBuilder.DropTable(
                name: "MealTags");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "PaymentInfo");

            migrationBuilder.AddColumn<string>(
                name: "TagsInput",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
