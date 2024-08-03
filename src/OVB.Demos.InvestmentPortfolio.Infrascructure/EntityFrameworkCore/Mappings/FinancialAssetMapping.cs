using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Mappings;

public sealed class FinancialAssetMapping : IEntityTypeConfiguration<FinancialAsset>
{
    public void Configure(EntityTypeBuilder<FinancialAsset> builder)
    {
        #region Table Configuration

        const string TABLE_NAME = "financial_assets";
        const string SCHEMA_NAME = "financial_asset";

        builder.ToTable(
            name: TABLE_NAME,
            schema: SCHEMA_NAME);

        #endregion

        #region Primary Key Configuration

        builder.HasKey(p => p.Id)
            .HasName("pk_financial_asset_id");

        #endregion

        #region Foreign Key Configuration

        builder.HasOne(p => p.Operator)
            .WithMany(p => p.FinancialAssets)
            .HasForeignKey(p => p.OperatorId)
            .HasPrincipalKey(p => p.Id)
            .HasConstraintName("fk_1_operator_n_financial_assets")
            .IsRequired(true);


        #endregion

        #region Index Key Configuration

        builder.HasIndex(p => p.Symbol)
            .IsUnique(true)
            .HasDatabaseName("uk_financial_asset_symbol");

        #endregion

        #region Properties Configuration

        builder.Property(p => p.Id)
            .IsRequired(true)
            .IsFixedLength(true)
            .HasColumnType("CHAR")
            .HasColumnName("idfinancial_asset")
            .HasMaxLength(Guid.Empty.ToString().Length)
            .HasConversion(p => p.GetIdentityAsString(), p => IdentityValueObject.Factory(Guid.Parse(p)))
            .ValueGeneratedNever();
        builder.Property(p => p.OperatorId)
            .IsRequired(true)
            .IsFixedLength(true)
            .HasColumnType("CHAR")
            .HasColumnName("operator_id")
            .HasMaxLength(Guid.Empty.ToString().Length)
            .HasConversion(p => p.GetIdentityAsString(), p => IdentityValueObject.Factory(Guid.Parse(p)))
            .ValueGeneratedNever();

        builder.Property(p => p.Symbol)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("symbol")
            .HasMaxLength(AssetSymbolValueObject.MAX_LENGTH)
            .HasConversion(p => p.GetSymbol(), p => AssetSymbolValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Description)
            .IsRequired(false)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("description")
            .HasMaxLength(DescriptionValueObject.MAX_LENGTH)
            .HasConversion(p => p.Value.GetDescription(), p => DescriptionValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.ExpirationDate)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DATE")
            .HasColumnName("expiration_date")
            .HasConversion(p => p.GetExpirationDate(), p => AssetExpirationDateValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Index)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("asset_index")
            .HasConversion(p => p.GetIndexAsString(), p => AssetIndexValueObject.Factory(p))
            .HasMaxLength(255)
            .ValueGeneratedNever();
        builder.Property(p => p.Status)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("status")
            .HasConversion(p => p.GetStatusAsString(), p => AssetStatusValueObject.Factory(p))
            .HasMaxLength(255)
            .ValueGeneratedNever();
        builder.Property(p => p.Type)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("type")
            .HasConversion(p => p.GetTypeAsString(), p => AssetTypeValueObject.Factory(p))
            .HasMaxLength(255)
            .ValueGeneratedNever();
        builder.Property(p => p.InterestRate)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DECIMAL(10, 5)")
            .HasColumnName("interest_rate")
            .HasConversion(p => p.GetInterestRate(), p => InterestRateValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.UnitaryPrice)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DECIMAL(10, 5)")
            .HasColumnName("unitary_price")
            .HasConversion(p => p.GetUnitaryPrice(), p => UnitaryPriceValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.QuantityAvailable)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("DECIMAL(10, 5)")
            .HasColumnName("quantity_available")
            .HasConversion(p => p.GetQuantityAvailable(), p => QuantityAvailableValueObject.Factory(p))
            .ValueGeneratedNever();

        #endregion
    }
}
