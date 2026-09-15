using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products_Crud.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierGstAndPaymentMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GstNumber",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMode",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GstNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PaymentMode",
                table: "Suppliers");
        }
    }
}
