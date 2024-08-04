namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OrderContext.Payloads;

public readonly struct CreateOrderPayloadInput
{
    public CreateOrderPayloadInput(decimal quantity)
    {
        Quantity = quantity;
    }

    public decimal Quantity { get; init; }
}
