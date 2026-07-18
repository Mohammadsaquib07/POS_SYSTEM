using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products_Crud.Migrations
{
    /// <inheritdoc />
    public partial class RenameSerachedItemsToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SerachedItems",
                table: "SerachedItems");

            migrationBuilder.RenameTable(
                name: "SerachedItems",
                newName: "ProductsList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsList",
                table: "ProductsList",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsList",
                table: "ProductsList");

            migrationBuilder.RenameTable(
                name: "ProductsList",
                newName: "SerachedItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SerachedItems",
                table: "SerachedItems",
                column: "Id");
        }
    }
}
