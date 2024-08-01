using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;

public sealed record Order
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderType Type { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitaryPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public Order(Guid id, DateTime createdAt, OrderType type, OrderStatus status, decimal quantity, decimal unitaryPrice, decimal totalPrice)
    {
        Id = id;
        CreatedAt = createdAt;
        Type = type;
        Status = status;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
        TotalPrice = totalPrice;
    }

    public Guid FinancialAssetId { get; set; }
    public FinancialAsset? FinancialAsset { get; set; }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
