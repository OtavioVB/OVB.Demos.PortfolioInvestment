using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OrderContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext;

public sealed class CreateExtractServiceInputValidationTests
{
    [Fact]
    public void CreateExtractServiceInput_Should_Be_Valid()
    {
        // Arrange
        var date = DateTime.UtcNow;
        ExtractTypeValueObject extractTypeAllowed = ExtractType.BUY;

        // Act
        var input = CreateExtractServiceInput.Factory(
            createdAt: date,
            type: extractTypeAllowed,
            totalPrice: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.TotalPrice,
            unitaryPrice: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.UnitaryPrice,
            quantity: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity,
            customerId: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId,
            financialAssetId: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.FinancialAssetId);

        // Assert
        Assert.Equal(date, input.CreatedAt);
        Assert.Equal(extractTypeAllowed.GetExtractType(), input.Type.GetExtractType());
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.TotalPrice.GetTotalPrice(), input.TotalPrice.GetTotalPrice());
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.UnitaryPrice.GetUnitaryPrice(), input.UnitaryPrice.GetUnitaryPrice());
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity.GetQuantity(), input.Quantity.GetQuantity());
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId, input.CustomerId);
        Assert.Equal(OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.FinancialAssetId, input.FinancialAssetId);
    }
}
