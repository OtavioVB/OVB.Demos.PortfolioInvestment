using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;

public sealed record Portfolio
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal ProftAndLoss { get; set; }

    public Portfolio(Guid id, decimal totalPrice, decimal quantity, decimal proftAndLoss)
    {
        Id = id;
        TotalPrice = totalPrice;
        Quantity = quantity;
        ProftAndLoss = proftAndLoss;
    }

    public Guid FinancialAssetId { get; set; }
    public FinancialAsset? FinancialAsset { get; set; }
    
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
