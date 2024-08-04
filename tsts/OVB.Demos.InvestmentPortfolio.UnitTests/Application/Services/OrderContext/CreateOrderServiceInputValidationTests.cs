using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Inputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OrderContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OrderContext;

public sealed class CreateOrderServiceInputValidationTests
{
    [Fact]
    public void CreateOrderServiceInput_Should_Be_Valid()
    {
        // Arrange

        // Act
        var input = CreateOrderServiceInput.Factory(
            customerId: CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id,
            type: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Type,
            quantity: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity,
            financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE);

        // Assert
        Assert.True(input.GetInputValidationtResult().IsSuccess);
        Assert.Equal(CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id, input.CustomerId);
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Type, input.Type);
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity, input.Quantity);
        Assert.Equal(FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE, input.FinancialAsset);
    }
}
