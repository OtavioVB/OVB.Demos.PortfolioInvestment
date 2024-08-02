using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;

public readonly struct UpdateFinancialAssetServiceOutput
{
    public FinancialAsset FinancialAsset { get; }

    private UpdateFinancialAssetServiceOutput(FinancialAsset financialAsset)
    {
        FinancialAsset = financialAsset;
    }

    public static UpdateFinancialAssetServiceOutput Factory(FinancialAsset financialAsset)
        => new(financialAsset);
}
