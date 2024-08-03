﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore;

#nullable disable

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240803163619_add_columns_created_at_and_password_hash_and_salt_on_table_customers")]
    partial class add_columns_created_at_and_password_hash_and_salt_on_table_customers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("idcustomer")
                        .IsFixedLength();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("code")
                        .IsFixedLength(false);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at")
                        .IsFixedLength(false);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("document")
                        .IsFixedLength(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("email")
                        .IsFixedLength(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("name")
                        .IsFixedLength(false);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("CHAR")
                        .HasColumnName("password_hash")
                        .IsFixedLength();

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("password_salt")
                        .IsFixedLength(false);

                    b.HasKey("Id")
                        .HasName("pk_customer_id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("uk_customer_code");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("uk_customer_email");

                    b.ToTable("customers", "customer");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject.Extract", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("idextract")
                        .IsFixedLength();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at")
                        .IsFixedLength(false);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("customer_id")
                        .IsFixedLength();

                    b.Property<string>("FinancialAssetId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("financial_asset_id")
                        .IsFixedLength();

                    b.Property<decimal>("Quantity")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("quantity")
                        .IsFixedLength(false);

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("total_price")
                        .IsFixedLength(false);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("type")
                        .IsFixedLength(false);

                    b.Property<decimal>("UnitaryPrice")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("unitary_price")
                        .IsFixedLength(false);

                    b.HasKey("Id")
                        .HasName("pk_extract_id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FinancialAssetId");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("uk_extract_id");

                    b.ToTable("extracts", "extract");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject.FinancialAsset", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("idfinancial_asset")
                        .IsFixedLength();

                    b.Property<string>("Description")
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("description")
                        .IsFixedLength(false);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("DATE")
                        .HasColumnName("expiration_date")
                        .IsFixedLength(false);

                    b.Property<string>("Index")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("asset_index")
                        .IsFixedLength(false);

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("interest_rate")
                        .IsFixedLength(false);

                    b.Property<string>("OperatorId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("operator_id")
                        .IsFixedLength();

                    b.Property<decimal>("QuantityAvailable")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("quantity_available")
                        .IsFixedLength(false);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("status")
                        .IsFixedLength(false);

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("symbol")
                        .IsFixedLength(false);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("type")
                        .IsFixedLength(false);

                    b.Property<decimal>("UnitaryPrice")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("unitary_price")
                        .IsFixedLength(false);

                    b.HasKey("Id")
                        .HasName("pk_financial_asset_id");

                    b.HasIndex("OperatorId");

                    b.HasIndex("Symbol")
                        .IsUnique()
                        .HasDatabaseName("uk_financial_asset_symbol");

                    b.ToTable("financial_assets", "financial_asset");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject.Operator", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("idoperator")
                        .IsFixedLength();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("code")
                        .IsFixedLength(false);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("document")
                        .IsFixedLength(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("email")
                        .IsFixedLength(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("name")
                        .IsFixedLength(false);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("CHAR")
                        .HasColumnName("password_hash")
                        .IsFixedLength();

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("password_salt")
                        .IsFixedLength(false);

                    b.HasKey("Id")
                        .HasName("pk_operator_id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("uk_operator_code");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("uk_operator_email");

                    b.ToTable("operators", "operator");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("idorder")
                        .IsFixedLength();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at")
                        .IsFixedLength(false);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("customer_id")
                        .IsFixedLength();

                    b.Property<string>("FinancialAssetId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("financial_asset_id")
                        .IsFixedLength();

                    b.Property<decimal>("Quantity")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("quantity")
                        .IsFixedLength(false);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("status")
                        .IsFixedLength(false);

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("total_price")
                        .IsFixedLength(false);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("type")
                        .IsFixedLength(false);

                    b.Property<decimal>("UnitaryPrice")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("unitary_price")
                        .IsFixedLength(false);

                    b.HasKey("Id")
                        .HasName("pk_order_id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FinancialAssetId");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("uk_order_id");

                    b.ToTable("orders", "order");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject.Portfolio", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("idportfolio")
                        .IsFixedLength();

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("customer_id")
                        .IsFixedLength();

                    b.Property<string>("FinancialAssetId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("CHAR")
                        .HasColumnName("financial_asset_id")
                        .IsFixedLength();

                    b.Property<decimal>("Quantity")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("quantity")
                        .IsFixedLength(false);

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("DECIMAL(10, 5)")
                        .HasColumnName("total_price")
                        .IsFixedLength(false);

                    b.HasKey("Id")
                        .HasName("pk_portfolio_id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FinancialAssetId");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("uk_portfolio_id");

                    b.ToTable("portfolios", "portfolio");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject.Extract", b =>
                {
                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject.Customer", "Customer")
                        .WithMany("Extracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_customer_n_extracts");

                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject.FinancialAsset", "FinancialAsset")
                        .WithMany("Extracts")
                        .HasForeignKey("FinancialAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_financial_asset_n_extracts");

                    b.Navigation("Customer");

                    b.Navigation("FinancialAsset");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject.FinancialAsset", b =>
                {
                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject.Operator", "Operator")
                        .WithMany("FinancialAssets")
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_operator_n_financial_assets");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject.Order", b =>
                {
                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_customer_n_orders");

                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject.FinancialAsset", "FinancialAsset")
                        .WithMany("Orders")
                        .HasForeignKey("FinancialAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_financial_asset_n_orders");

                    b.Navigation("Customer");

                    b.Navigation("FinancialAsset");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject.Portfolio", b =>
                {
                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject.Customer", "Customer")
                        .WithMany("Portfolios")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_customer_n_portfolios");

                    b.HasOne("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject.FinancialAsset", "FinancialAsset")
                        .WithMany("Portfolios")
                        .HasForeignKey("FinancialAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_1_financial_asset_n_portfolios");

                    b.Navigation("Customer");

                    b.Navigation("FinancialAsset");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject.Customer", b =>
                {
                    b.Navigation("Extracts");

                    b.Navigation("Orders");

                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject.FinancialAsset", b =>
                {
                    b.Navigation("Extracts");

                    b.Navigation("Orders");

                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject.Operator", b =>
                {
                    b.Navigation("FinancialAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
