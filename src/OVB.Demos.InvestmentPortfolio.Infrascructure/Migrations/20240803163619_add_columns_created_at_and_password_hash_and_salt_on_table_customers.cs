using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.Migrations
{
    /// <inheritdoc />
    public partial class add_columns_created_at_and_password_hash_and_salt_on_table_customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "customer",
                table: "customers",
                type: "TIMESTAMPTZ",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                schema: "customer",
                table: "customers",
                type: "CHAR(64)",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password_salt",
                schema: "customer",
                table: "customers",
                type: "VARCHAR",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "customer",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "password_hash",
                schema: "customer",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "password_salt",
                schema: "customer",
                table: "customers");
        }
    }
}
