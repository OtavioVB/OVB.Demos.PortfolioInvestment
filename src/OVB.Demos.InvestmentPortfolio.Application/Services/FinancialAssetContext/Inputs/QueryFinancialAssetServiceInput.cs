using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;

public readonly struct QueryFinancialAssetServiceInput
{
    public PageValueObject Page { get; }
    public OffsetValueObject Offset { get; }

    private QueryFinancialAssetServiceInput(PageValueObject page, OffsetValueObject offset)
    {
        Page = page;
        Offset = offset;
    }

    public static QueryFinancialAssetServiceInput Factory(PageValueObject page, OffsetValueObject offset)
        => new(page, offset);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(Page, Offset);
}
