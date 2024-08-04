using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Outputs;

public readonly struct CreateOrderUseCaseOutput
{
    public Order Order { get; }
    public FinancialAsset FinancialAsset { get; }

    private CreateOrderUseCaseOutput(Order order, FinancialAsset financialAsset)
    {
        Order = order;
        FinancialAsset = financialAsset;
    }

    public static CreateOrderUseCaseOutput Factory(Order order, FinancialAsset financialAsset)
        => new(order, financialAsset);
}
