using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Inputs;

public readonly struct CreateOrderServiceInput
{
    public IdentityValueObject CustomerId { get; }
    public OrderTypeValueObject Type { get; }
    public QuantityValueObject Quantity { get; }
    public FinancialAsset FinancialAsset { get; }

    private CreateOrderServiceInput(IdentityValueObject customerId,  OrderTypeValueObject type, QuantityValueObject quantity,
        FinancialAsset financialAsset)
    {
        CustomerId = customerId;
        Type = type;
        Quantity = quantity;
        FinancialAsset = financialAsset;
    }

    public static CreateOrderServiceInput Factory(IdentityValueObject customerId, OrderTypeValueObject type, QuantityValueObject quantity, FinancialAsset financialAsset)
        => new(customerId, type, quantity, financialAsset);

    public MethodResult<INotification> GetInputValidationtResult()
        => MethodResult<INotification>.Factory(CustomerId, Type, Quantity);
}
