using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;

public sealed record Order
{
    public IdentityValueObject Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderTypeValueObject Type { get; set; }
    public OrderStatusValueObject Status { get; set; }
    public QuantityValueObject Quantity { get; set; }
    public UnitaryPriceValueObject UnitaryPrice { get; set; }
    public TotalPriceValueObject TotalPrice { get; set; }

    public Order(IdentityValueObject id, DateTime createdAt, OrderTypeValueObject type, OrderStatusValueObject status,
        QuantityValueObject quantity, UnitaryPriceValueObject unitaryPrice, TotalPriceValueObject totalPrice)
    {
        Id = id;
        CreatedAt = createdAt;
        Type = type;
        Status = status;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
        TotalPrice = totalPrice;
    }

    public IdentityValueObject FinancialAssetId { get; set; }
    public FinancialAsset? FinancialAsset { get; set; }

    public IdentityValueObject CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
