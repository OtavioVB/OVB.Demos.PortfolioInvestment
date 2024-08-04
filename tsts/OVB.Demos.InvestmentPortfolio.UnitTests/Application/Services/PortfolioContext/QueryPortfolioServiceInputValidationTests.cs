using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext;

public sealed class QueryPortfolioServiceInputValidationTests
{
    [Fact]
    public void Query_Portfolios_Service_Input_Should_Be_Valid()
    {
        // Arrange
        var customerId = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id;
        var page = PageValueObject.Factory(1);
        var offset = OffsetValueObject.Factory(30);

        // Act
        var input = QueryPortfolioServiceInput.Factory(
            customerId: customerId,
            page: page,
            offset: offset);

        // Assert
        Assert.True(input.GetInputValidationResult().IsSuccess);
        Assert.Equal(customerId, input.CustomerId);
        Assert.Equal(page.GetPage(), input.Page.GetPage());
        Assert.Equal(offset.GetOffset(), input.Offset.GetOffset());
    }
}
