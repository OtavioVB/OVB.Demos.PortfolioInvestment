using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.PortfolioContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext;

public sealed class CreateOrUpdatePortfolioServiceOutputValidationTests
{
    [Fact]
    public void CreateOrUpdatePortfolioServiceOutput_Should_Be_Valid()
    {
        // Arrange

        // Act
        var output = CreateOrUpdatePortfolioServiceOutput.Factory(
            portfolio: PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS);

        // Assert
        Assert.Equal(PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS, output.Portfolio);
    }
}
