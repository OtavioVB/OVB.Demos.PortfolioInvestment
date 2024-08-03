using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class QueryFinancialAssetServiceInputValidationTests
{
    [Fact]
    public void QueryFinancialAssetServiceInput_Should_Be_Valid()
    {
        // Arrange
        var page = 5;
        var offset = 25;

        // Act
        var input = QueryFinancialAssetServiceInput.Factory(
            page: page,
            offset: offset);

        // Assert
        Assert.True(input.GetInputValidationResult().IsSuccess);
        Assert.Empty(input.GetInputValidationResult().Notifications);
        Assert.Equal<int>(page, input.Page);
        Assert.Equal<int>(offset, input.Offset);
    }
}
