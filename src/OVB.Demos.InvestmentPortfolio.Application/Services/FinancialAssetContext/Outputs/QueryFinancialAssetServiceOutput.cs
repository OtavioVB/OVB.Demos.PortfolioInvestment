using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;

public readonly struct QueryFinancialAssetServiceOutput
{
    public PageValueObject Page { get; }
    public OffsetValueObject Offset { get; }
    public FinancialAsset[] FinancialAssets { get; }

    private QueryFinancialAssetServiceOutput(PageValueObject page, OffsetValueObject offset, FinancialAsset[] financialAssets)
    {
        Page = page;
        Offset = offset;
        FinancialAssets = financialAssets;
    }

    public static QueryFinancialAssetServiceOutput Factory(PageValueObject page, OffsetValueObject offset, FinancialAsset[] financialAssets)
        => new(page, offset, financialAssets);
}
