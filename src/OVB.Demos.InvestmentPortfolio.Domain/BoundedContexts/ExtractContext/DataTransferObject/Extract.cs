using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;

public sealed record Extract
{
    public IdentityValueObject Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public ExtractTypeValueObject Type { get; set; }
    public TotalPriceValueObject TotalPrice { get; set; }
    public UnitaryPriceValueObject UnitaryPrice { get; set; }
    public QuantityAvailableValueObject Quantity { get; set; }

    public Extract(IdentityValueObject id, DateTime createdAt, ExtractTypeValueObject type, TotalPriceValueObject totalPrice, UnitaryPriceValueObject unitaryPrice, QuantityAvailableValueObject quantity)
    {
        Id = id;
        CreatedAt = createdAt;
        Type = type;
        TotalPrice = totalPrice;
        UnitaryPrice = unitaryPrice;
        Quantity = quantity;
    }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public Guid FinancialAssetId { get; set; }
    public FinancialAsset? FinancialAsset { get; set; }
}
