using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;

public sealed record Portfolio
{
    public IdentityValueObject Id { get; set; }
    public TotalPriceValueObject TotalPrice { get; set; }
    public QuantityValueObject Quantity { get; set; }
    public decimal ProftAndLoss { get; set; }

    public Portfolio(IdentityValueObject id, TotalPriceValueObject totalPrice, QuantityValueObject quantity, decimal proftAndLoss)
    {
        Id = id;
        TotalPrice = totalPrice;
        Quantity = quantity;
        ProftAndLoss = proftAndLoss;
    }

    public IdentityValueObject FinancialAssetId { get; set; }
    public FinancialAsset? FinancialAsset { get; set; }
    
    public IdentityValueObject CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
