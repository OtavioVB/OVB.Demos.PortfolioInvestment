using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.Migrations
{
    /// <inheritdoc />
    public partial class add_database_default_mapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.EnsureSchema(
                name: "extract");

            migrationBuilder.EnsureSchema(
                name: "financial_asset");

            migrationBuilder.EnsureSchema(
                name: "operator");

            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.EnsureSchema(
                name: "portfolio");

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "customer",
                columns: table => new
                {
                    idcustomer = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    code = table.Column<string>(type: "VARCHAR", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: false),
                    document = table.Column<string>(type: "VARCHAR", maxLength: 14, nullable: false),
                    email = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_id", x => x.idcustomer);
                });

            migrationBuilder.CreateTable(
                name: "operators",
                schema: "operator",
                columns: table => new
                {
                    idoperator = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    code = table.Column<string>(type: "VARCHAR", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: false),
                    email = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "CHAR(64)", fixedLength: true, maxLength: 64, nullable: false),
                    password_salt = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: false),
                    document = table.Column<string>(type: "VARCHAR", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_operator_id", x => x.idoperator);
                });

            migrationBuilder.CreateTable(
                name: "financial_assets",
                schema: "financial_asset",
                columns: table => new
                {
                    idfinancial_asset = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    symbol = table.Column<string>(type: "VARCHAR", maxLength: 32, nullable: false),
                    description = table.Column<string>(type: "VARCHAR", maxLength: 64, nullable: true),
                    expiration_date = table.Column<DateTime>(type: "DATE", nullable: false),
                    asset_index = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    interest_rate = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    unitary_price = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    quantity_available = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    operator_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financial_asset_id", x => x.idfinancial_asset);
                    table.ForeignKey(
                        name: "fk_1_operator_n_financial_assets",
                        column: x => x.operator_id,
                        principalSchema: "operator",
                        principalTable: "operators",
                        principalColumn: "idoperator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "extracts",
                schema: "extract",
                columns: table => new
                {
                    idextract = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    type = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    unitary_price = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    customer_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    financial_asset_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_extract_id", x => x.idextract);
                    table.ForeignKey(
                        name: "fk_1_customer_n_extracts",
                        column: x => x.customer_id,
                        principalSchema: "customer",
                        principalTable: "customers",
                        principalColumn: "idcustomer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_1_financial_asset_n_extracts",
                        column: x => x.financial_asset_id,
                        principalSchema: "financial_asset",
                        principalTable: "financial_assets",
                        principalColumn: "idfinancial_asset",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "order",
                columns: table => new
                {
                    idorder = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    type = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    unitary_price = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    financial_asset_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    customer_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_id", x => x.idorder);
                    table.ForeignKey(
                        name: "fk_1_customer_n_orders",
                        column: x => x.customer_id,
                        principalSchema: "customer",
                        principalTable: "customers",
                        principalColumn: "idcustomer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_1_financial_asset_n_orders",
                        column: x => x.financial_asset_id,
                        principalSchema: "financial_asset",
                        principalTable: "financial_assets",
                        principalColumn: "idfinancial_asset",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "portfolios",
                schema: "portfolio",
                columns: table => new
                {
                    idportfolio = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(10,5)", nullable: false),
                    financial_asset_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false),
                    customer_id = table.Column<string>(type: "CHAR(36)", fixedLength: true, maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_portfolio_id", x => x.idportfolio);
                    table.ForeignKey(
                        name: "fk_1_customer_n_portfolios",
                        column: x => x.customer_id,
                        principalSchema: "customer",
                        principalTable: "customers",
                        principalColumn: "idcustomer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_1_financial_asset_n_portfolios",
                        column: x => x.financial_asset_id,
                        principalSchema: "financial_asset",
                        principalTable: "financial_assets",
                        principalColumn: "idfinancial_asset",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "uk_customer_code",
                schema: "customer",
                table: "customers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_customer_email",
                schema: "customer",
                table: "customers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_extracts_customer_id",
                schema: "extract",
                table: "extracts",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_extracts_financial_asset_id",
                schema: "extract",
                table: "extracts",
                column: "financial_asset_id");

            migrationBuilder.CreateIndex(
                name: "uk_extract_id",
                schema: "extract",
                table: "extracts",
                column: "idextract",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_financial_assets_operator_id",
                schema: "financial_asset",
                table: "financial_assets",
                column: "operator_id");

            migrationBuilder.CreateIndex(
                name: "uk_financial_asset_symbol",
                schema: "financial_asset",
                table: "financial_assets",
                column: "symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_operator_code",
                schema: "operator",
                table: "operators",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_operator_email",
                schema: "operator",
                table: "operators",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                schema: "order",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_financial_asset_id",
                schema: "order",
                table: "orders",
                column: "financial_asset_id");

            migrationBuilder.CreateIndex(
                name: "uk_order_id",
                schema: "order",
                table: "orders",
                column: "idorder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_portfolios_customer_id",
                schema: "portfolio",
                table: "portfolios",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_portfolios_financial_asset_id",
                schema: "portfolio",
                table: "portfolios",
                column: "financial_asset_id");

            migrationBuilder.CreateIndex(
                name: "uk_portfolio_id",
                schema: "portfolio",
                table: "portfolios",
                column: "idportfolio",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "extracts",
                schema: "extract");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "order");

            migrationBuilder.DropTable(
                name: "portfolios",
                schema: "portfolio");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "financial_assets",
                schema: "financial_asset");

            migrationBuilder.DropTable(
                name: "operators",
                schema: "operator");
        }
    }
}
