using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class QueryFinancialAssetServiceOutputValidationTests
{
    [Fact]
    public void QueryFinancialAssetServiceOutput_Should_Be_Valid()
    {
        // Arrange
        var page = 5;
        var offset = 25;
        
        // Act
        var output = QueryFinancialAssetServiceOutput.Factory(
            page: page,
            offset: offset,
            financialAssets: [FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE]);

        // Assert
        Assert.Equal([FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE], output.FinancialAssets);
        Assert.Equal<int>(page, output.Page);
        Assert.Equal<int>(offset, output.Offset);
    }
}
