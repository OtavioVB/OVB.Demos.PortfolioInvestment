using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;

public readonly struct QueryByIdFinancialAssetServiceInput
{
    public IdentityValueObject FinancialAssetId { get; }

    private QueryByIdFinancialAssetServiceInput(IdentityValueObject financialAssetId)
    {
        FinancialAssetId = financialAssetId;
    }

    public static QueryByIdFinancialAssetServiceInput Factory(IdentityValueObject financialAssetId)
        => new(financialAssetId);

    public MethodResult<INotification> GetValidationResult()
        => MethodResult<INotification>.Factory(FinancialAssetId);
}
