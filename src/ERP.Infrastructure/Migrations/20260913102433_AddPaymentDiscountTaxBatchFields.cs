using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products_Crud.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentDiscountTaxBatchFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "PurchaseInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceDue",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMode",
                table: "PurchaseInvoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTax",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "PurchaseInvoiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercent",
                table: "PurchaseInvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "PurchaseInvoiceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercent",
                table: "PurchaseInvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "BalanceDue",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "PaymentMode",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "TotalTax",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropColumn(
                name: "TaxPercent",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Products");
        }
    }
}
