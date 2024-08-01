using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;

public sealed record Extract
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public ExtractType Type { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal UnitaryPrice { get; set; }
    public decimal Quantity { get; set; }

    public Extract(Guid id, DateTime createdAt, ExtractType type, decimal totalPrice, decimal unitaryPrice, decimal quantity)
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
