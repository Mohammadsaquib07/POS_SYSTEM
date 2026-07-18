using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products_Crud.Migrations
{
    /// <inheritdoc />
    public partial class SearchedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "SerachedItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SerachedItems",
                table: "SerachedItems",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SerachedItems",
                table: "SerachedItems");

            migrationBuilder.RenameTable(
                name: "SerachedItems",
                newName: "products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");
        }
    }
}
