using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext;


public sealed class QueryExtractServiceInputValidationTests
{
    [Fact]
    public void Query_Extract_Service_Input_Should_Be_Valid()
    {
        // Arrange
        var customerId = IdentityValueObject.Factory();
        var page = 1;
        var offset = 25;

        // Act
        var input = QueryExtractServiceInput.Factory(
            customerId: customerId,
            page: page,
            offset: offset);

        // Assert
        Assert.True(input.GetInputValidationResult().IsSuccess);
        Assert.Equal(page, input.Page.GetPage());
        Assert.Equal(offset, input.Offset.GetOffset());
    }
}
