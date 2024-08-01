using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

public sealed record FinancialAsset
{
    public IdentityValueObject Id { get; set; }
    public AssetSymbolValueObject Symbol { get; set; }
    public DescriptionValueObject Description { get; set; }
    public AssetExpirationDateValueObject ExpirationDate { get; set; }
    public AssetIndexValueObject Index { get; set; }
    public AssetTypeValueObject Type { get; set; }
    public AssetStatusValueObject Status { get; set; }
    public InterestRateValueObject InterestRate { get; set; }
    public UnitaryPriceValueObject UnitaryPrice { get; set; }
    public QuantityAvailableValueObject QuantityAvailable { get; set; }

    public FinancialAsset(
        IdentityValueObject id, AssetSymbolValueObject symbol, 
        DescriptionValueObject description, AssetExpirationDateValueObject expirationDate, AssetIndexValueObject index, AssetTypeValueObject type, 
        AssetStatusValueObject status, InterestRateValueObject interestRate, UnitaryPriceValueObject unitaryPrice, QuantityAvailableValueObject quantityAvailable)
    {
        Id = id;
        Symbol = symbol;
        Description = description;
        ExpirationDate = expirationDate;
        Index = index;
        Type = type;
        Status = status;
        InterestRate = interestRate;
        UnitaryPrice = unitaryPrice;
        QuantityAvailable = quantityAvailable;
    }

    public IdentityValueObject OperatorId { get; set; }
    public Operator? Operator { get; set; }

    public IList<Order>? Orders { get; set; }
    public IList<Extract>? Extracts { get; set; }
}
