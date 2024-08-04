using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Outputs;

public readonly struct CreateOrderServiceOutput
{
    public Order? Order { get; }

    private CreateOrderServiceOutput(Order? order)
    {
        Order = order;
    }

    public static CreateOrderServiceOutput Factory(Order? order)
        => new(order);
}
