using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.PortfolioContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext;

public sealed class QueryPorfolioServiceOutputValidationTests
{
    [Fact]
    public void Query_Portfolios_Service_Output_Should_Be_Valid()
    {
        // Arrange
        var page = PageValueObject.Factory(1);
        var offset = OffsetValueObject.Factory(30);
        Portfolio[] portfolios = [PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS];

        // Act
        var output = QueryPortfolioServiceOutput.Factory(
            page: page,
            offset: offset,
            items: portfolios);

        // Assert
        Assert.Equal(portfolios, output.Items);
        Assert.Equal(page.GetPage(), output.Page.GetPage());
        Assert.Equal(offset.GetOffset(), output.Offset.GetOffset());
    }
}
