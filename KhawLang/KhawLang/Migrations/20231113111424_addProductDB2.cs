using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhawLang.Migrations
{
    /// <inheritdoc />
    public partial class addProductDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Meals");

            migrationBuilder.AddColumn<string>(
                name: "TagsInput",
                table: "Meals",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsInput",
                table: "Meals");

            migrationBuilder.AddColumn<List<string>>(
                name: "Tags",
                table: "Meals",
                type: "text[]",
                nullable: true);
        }
    }
}
