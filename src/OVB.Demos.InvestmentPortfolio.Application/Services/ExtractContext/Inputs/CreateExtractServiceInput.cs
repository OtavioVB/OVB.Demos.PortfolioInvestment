using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;

public readonly struct CreateExtractServiceInput
{
    public DateTime CreatedAt { get; }
    public ExtractTypeValueObject Type { get; }
    public TotalPriceValueObject TotalPrice { get; }
    public UnitaryPriceValueObject UnitaryPrice { get; }
    public QuantityValueObject Quantity { get; }
    public IdentityValueObject CustomerId { get; }
    public IdentityValueObject FinancialAssetId { get; }

    private CreateExtractServiceInput(DateTime createdAt, ExtractTypeValueObject type, TotalPriceValueObject totalPrice, UnitaryPriceValueObject unitaryPrice, 
        QuantityValueObject quantity, IdentityValueObject customerId, IdentityValueObject financialAssetId)
    {
        CreatedAt = createdAt;
        Type = type;
        TotalPrice = totalPrice;
        UnitaryPrice = unitaryPrice;
        Quantity = quantity;
        CustomerId = customerId;
        FinancialAssetId = financialAssetId;
    }

    public static CreateExtractServiceInput Factory(DateTime createdAt, ExtractTypeValueObject type, TotalPriceValueObject totalPrice, UnitaryPriceValueObject unitaryPrice,
        QuantityValueObject quantity, IdentityValueObject customerId, IdentityValueObject financialAssetId)
        => new(createdAt, type, totalPrice, unitaryPrice, quantity, customerId, financialAssetId);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(Type, TotalPrice, UnitaryPrice, Quantity, CustomerId, FinancialAssetId);
}
