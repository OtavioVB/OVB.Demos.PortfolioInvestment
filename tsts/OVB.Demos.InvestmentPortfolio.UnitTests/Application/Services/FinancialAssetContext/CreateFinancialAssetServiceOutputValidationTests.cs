using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class CreateFinancialAssetServiceOutputValidationTests
{
    [Fact]
    public void CreateFinancialAssetServiceOutput_Should_Be_Valid()
    {
        // Arrange

        // Act
        var output = CreateFinancialAssetServiceOutput.Factory(
            financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE);

        // Assert
        Assert.Equal(FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE, output.FinancialAsset);
    }
}
