using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;

public readonly struct UpdateFinancialAssetServiceInput
{
    public IdentityValueObject OperatorId { get; }
    public IdentityValueObject FinancialAssetId { get; }
    public AssetSymbolValueObject? Symbol { get; }
    public DescriptionValueObject Description { get; }
    public AssetExpirationDateValueObject? ExpirationDate { get; }
    public AssetStatusValueObject? Status { get; }
    public InterestRateValueObject? InterestRate { get; }
    public UnitaryPriceValueObject? UnitaryPrice { get; }
    public QuantityAvailableValueObject? QuantityAvailable { get; }

    private UpdateFinancialAssetServiceInput(IdentityValueObject operatorId, IdentityValueObject financialAssetId, AssetSymbolValueObject? symbol, DescriptionValueObject description, 
        AssetExpirationDateValueObject? expirationDate, AssetStatusValueObject? status, InterestRateValueObject? interestRate, 
        UnitaryPriceValueObject? unitaryPrice, QuantityAvailableValueObject? quantityAvailable)
    {
        OperatorId = operatorId;
        FinancialAssetId = financialAssetId;
        Symbol = symbol;
        Description = description;
        ExpirationDate = expirationDate;
        Status = status;
        InterestRate = interestRate;
        UnitaryPrice = unitaryPrice;
        QuantityAvailable = quantityAvailable;
    }

    public static UpdateFinancialAssetServiceInput Factory(IdentityValueObject operatorId, IdentityValueObject financialAssetId, AssetSymbolValueObject? symbol, DescriptionValueObject description,
        AssetExpirationDateValueObject? expirationDate, AssetStatusValueObject? status, InterestRateValueObject? interestRate,
        UnitaryPriceValueObject? unitaryPrice, QuantityAvailableValueObject? quantityAvailable)
        => new(operatorId, financialAssetId, symbol, description, expirationDate, status, interestRate, unitaryPrice, quantityAvailable);

    public MethodResult<INotification> GetInputValidationResult() 
        => MethodResult<INotification>.Factory(OperatorId, FinancialAssetId, 
            Symbol ?? MethodResult<INotification>.FactorySuccess(), Description, 
            ExpirationDate ?? MethodResult<INotification>.FactorySuccess(), 
            Status ?? MethodResult<INotification>.FactorySuccess(),
            InterestRate ?? MethodResult<INotification>.FactorySuccess(), 
            UnitaryPrice ?? MethodResult<INotification>.FactorySuccess(), 
            QuantityAvailable ?? MethodResult<INotification>.FactorySuccess());
}
