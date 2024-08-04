using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Inputs;

public readonly struct CreateOrderUseCaseInput
{
    public IdentityValueObject CustomerId { get; }
    public IdentityValueObject FinancialAssetId { get; }
    public OrderTypeValueObject Type { get; }
    public QuantityValueObject Quantity { get; }

    private CreateOrderUseCaseInput(IdentityValueObject customerId, IdentityValueObject financialAssetId, OrderTypeValueObject type, QuantityValueObject quantity)
    {
        CustomerId = customerId;
        FinancialAssetId = financialAssetId;
        Type = type;
        Quantity = quantity;
    }

    public static CreateOrderUseCaseInput Factory(IdentityValueObject customerId, IdentityValueObject financialAssetId, OrderTypeValueObject type,
        QuantityValueObject quantity)
        => new(customerId, financialAssetId, type, quantity);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(CustomerId, FinancialAssetId, Type, Quantity);
}
