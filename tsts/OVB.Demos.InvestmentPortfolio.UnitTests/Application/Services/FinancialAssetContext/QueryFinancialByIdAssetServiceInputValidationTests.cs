using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class QueryFinancialByIdAssetServiceInputValidationTests
{
    [Fact]
    public void QueryFinancialByIdAssetServiceInput_Should_Be_Valid()
    {
        // Arrange
        var id = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE.Id;

        // Act
        var input = QueryByIdFinancialAssetServiceInput.Factory(
            financialAssetId: id);

        // Assert
        Assert.True(input.GetValidationResult().IsSuccess);
        Assert.Equal(input.FinancialAssetId, id);
    }
}
