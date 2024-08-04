using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;

public readonly struct QueryByIdFinancialAssetServiceOutput
{
    public FinancialAsset FinancialAsset { get; }

    private QueryByIdFinancialAssetServiceOutput(FinancialAsset financialAsset)
    {
        FinancialAsset = financialAsset;
    }

    public static QueryByIdFinancialAssetServiceOutput Factory(FinancialAsset financialAsset)
        => new(financialAsset);
}
