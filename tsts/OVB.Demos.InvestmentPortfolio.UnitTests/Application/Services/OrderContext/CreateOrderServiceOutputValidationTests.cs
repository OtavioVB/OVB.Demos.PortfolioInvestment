using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Outputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OrderContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OrderContext;

public sealed class CreateOrderServiceOutputValidationTests
{
    [Fact]
    public void Create_Order_Service_Output_Should_Be_Valid()
    {
        // Arrange

        // Act
        var output = CreateOrderServiceOutput.Factory(
            order: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE);

        // Assert
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE, output.Order);
    }
}
