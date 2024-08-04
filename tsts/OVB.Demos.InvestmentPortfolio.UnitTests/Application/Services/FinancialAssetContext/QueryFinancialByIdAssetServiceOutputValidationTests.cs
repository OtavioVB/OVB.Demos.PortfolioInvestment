using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class QueryFinancialByIdAssetServiceOutputValidationTests
{
    [Fact]
    public void QueryFinancialByIdAssetServiceOutput_Should_Be_Valid()
    {
        // Arrange

        // Act
        var output = QueryByIdFinancialAssetServiceOutput.Factory(
            financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE);

        // Assert
        Assert.Equal(output.FinancialAsset, FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE);
    }
}
