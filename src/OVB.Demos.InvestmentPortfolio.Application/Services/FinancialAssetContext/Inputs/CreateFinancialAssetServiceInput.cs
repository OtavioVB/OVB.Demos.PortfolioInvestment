using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;

public readonly struct CreateFinancialAssetServiceInput
{
    public IdentityValueObject OperatorId { get; }
    public AssetSymbolValueObject Symbol { get; }
    public DescriptionValueObject Description { get; }
    public AssetExpirationDateValueObject ExpirationDate { get; }
    public AssetIndexValueObject Index { get; }
    public AssetTypeValueObject Type { get; }
    public AssetStatusValueObject Status { get; }
    public InterestRateValueObject InterestRate { get; }
    public UnitaryPriceValueObject UnitaryPrice { get; }
    public QuantityAvailableValueObject QuantityAvailable { get; }

    private CreateFinancialAssetServiceInput(IdentityValueObject operatorId, AssetSymbolValueObject symbol, DescriptionValueObject description,
        AssetExpirationDateValueObject expirationDate, AssetIndexValueObject index, AssetTypeValueObject type, AssetStatusValueObject status, InterestRateValueObject interestRate,
        UnitaryPriceValueObject unitaryPrice, QuantityAvailableValueObject quantityAvailable)
    {
        OperatorId = operatorId;
        Symbol = symbol;
        Description = description;
        ExpirationDate = expirationDate;
        Index = index;
        Type = type;
        Status = status;
        InterestRate = interestRate;
        UnitaryPrice = unitaryPrice;
        QuantityAvailable = quantityAvailable;
    }

    public static CreateFinancialAssetServiceInput Factory(IdentityValueObject operatorId, AssetSymbolValueObject symbol, DescriptionValueObject description,
        AssetExpirationDateValueObject expirationDate, AssetIndexValueObject index, AssetTypeValueObject type, AssetStatusValueObject status, InterestRateValueObject interestRate,
        UnitaryPriceValueObject unitaryPrice, QuantityAvailableValueObject quantityAvailable)
        => new(operatorId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(OperatorId, Symbol, Description, ExpirationDate, Index, Type, Status, InterestRate, UnitaryPrice, QuantityAvailable);
}
