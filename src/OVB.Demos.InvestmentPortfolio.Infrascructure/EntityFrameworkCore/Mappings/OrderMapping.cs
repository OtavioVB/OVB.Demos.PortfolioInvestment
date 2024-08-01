using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Mappings;

public sealed class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        #region Table Configuration

        builder.ToTable(
            name: "orders",
            schema: "order");

        #endregion

        #region Primary Key Configuration

        builder.HasKey(p => p.Id)
            .HasName("pk_order_id");

        #endregion

        #region Foreign Key Configuration

        builder.HasOne(p => p.FinancialAsset)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.FinancialAssetId)
            .HasPrincipalKey(p => p.Id)
            .HasConstraintName("fk_1_financial_asset_n_orders")
            .IsRequired(true);

        builder.HasOne(p => p.Customer)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.CustomerId)
            .HasPrincipalKey(p => p.Id)
            .HasConstraintName("fk_1_customer_n_orders")
            .IsRequired(true);

        #endregion

        #region Index Key Configuration

        builder.HasIndex(p => p.Id)
            .IsUnique(true)
            .HasDatabaseName("uk_order_id");

        #endregion

        #region Properties Configuration

        builder.Property(p => p.Id)
            .IsRequired(true)
            .IsFixedLength(true)
            .HasColumnType("CHAR")
            .HasColumnName("idextract")
            .HasMaxLength(Guid.Empty.ToString().Length)
            .HasConversion(p => p.GetIdentityAsString(), p => IdentityValueObject.Factory(Guid.Parse(p)))
            .ValueGeneratedNever();
        builder.Property(p => p.CustomerId)
            .IsRequired(true)
            .IsFixedLength(true)
            .HasColumnType("CHAR")
            .HasColumnName("customer_id")
            .HasMaxLength(Guid.Empty.ToString().Length)
            .HasConversion(p => p.GetIdentityAsString(), p => IdentityValueObject.Factory(Guid.Parse(p)))
            .ValueGeneratedNever();
        builder.Property(p => p.FinancialAssetId)
            .IsRequired(true)
            .IsFixedLength(true)
            .HasColumnType("CHAR")
            .HasColumnName("financial_asset_id")
            .HasMaxLength(Guid.Empty.ToString().Length)
            .HasConversion(p => p.GetIdentityAsString(), p => IdentityValueObject.Factory(Guid.Parse(p)))
            .ValueGeneratedNever();

        builder.Property(p => p.CreatedAt)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("TIMESTAMPTZ")
            .HasColumnName("created_at")
            .ValueGeneratedNever();
        builder.Property(p => p.Type)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("type")
            .HasMaxLength(255)
            .HasConversion(p => p.GetOrderTypeAsString(), p => OrderTypeValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Status)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("status")
            .HasMaxLength(255)
            .HasConversion(p => p.GetOrderStatusAsString(), p => OrderStatusValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.UnitaryPrice)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DECIMAL(10, 5)")
            .HasColumnName("unitary_price")
            .HasConversion(p => p.GetUnitaryPrice(), p => UnitaryPriceValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.TotalPrice)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DECIMAL(10, 5)")
            .HasColumnName("total_price")
            .HasConversion(p => p.GetTotalPrice(), p => TotalPriceValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Quantity)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DECIMAL(10, 5)")
            .HasColumnName("quantity")
            .HasConversion(p => p.GetQuantity(), p => QuantityValueObject.Factory(p))
            .ValueGeneratedNever();

        #endregion
    }
}
