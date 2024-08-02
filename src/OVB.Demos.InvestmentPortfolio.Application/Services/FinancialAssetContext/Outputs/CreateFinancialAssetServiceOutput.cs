using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;

public readonly struct CreateFinancialAssetServiceOutput
{
    public FinancialAsset FinancialAsset { get; }

    private CreateFinancialAssetServiceOutput(FinancialAsset financialAsset)
    {
        FinancialAsset = financialAsset;
    }

    public static CreateFinancialAssetServiceOutput Factory(FinancialAsset financialAsset)
        => new(financialAsset);
}
