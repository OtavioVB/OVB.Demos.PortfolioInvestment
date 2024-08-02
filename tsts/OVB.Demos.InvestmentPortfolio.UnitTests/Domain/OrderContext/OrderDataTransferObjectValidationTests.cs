using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OrderContext;

public sealed class OrderDataTransferObjectValidationTests
{
    public static Order ORDER_EXAMPLE = new Order(
        id: Guid.NewGuid(),
        createdAt: DateTime.Parse("2024-07-31T09:48:45Z"),
        type: OrderType.BUY,
        status: OrderStatus.EXECUTED,
        quantity: 3,
        unitaryPrice: 5,
        totalPrice: 15)
    {
        FinancialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE,
        Customer = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE,
    };

    [Theory]
    [InlineData("2024-07-31T09:48:45Z", OrderType.BUY, OrderStatus.EXECUTED, 3, 5, 15)]
    public void Order_Data_Transfer_Object_Should_Be_Equal_Expected(string createdAt, OrderType type, OrderStatus status, decimal quantity, decimal unitaryPrice, decimal totalPrice)
    {
        // Arrange
        var createdAtDate = DateTime.Parse(createdAt);
        var orderId = Guid.NewGuid();

        // Act
        var order = new Order(
            id: orderId,
            createdAt: createdAtDate,
            type: type,
            status: status,
            quantity: quantity,
            unitaryPrice: unitaryPrice,
            totalPrice: totalPrice)
        {
            FinancialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE,
            Customer = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE
        };

        // Assert
        Assert.Equal(orderId, order.Id);
        Assert.Equal(createdAtDate, order.CreatedAt);
        Assert.Equal(type, order.Type.GetOrderType());
        Assert.Equal(status, order.Status.GetOrderStatus());
        Assert.Equal(quantity, order.Quantity.GetQuantity());
        Assert.Equal(unitaryPrice, order.UnitaryPrice.GetUnitaryPrice());
        Assert.Equal(totalPrice, order.TotalPrice.GetTotalPrice());
        Assert.Equal(FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE, order.FinancialAsset);
        Assert.Equal(CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE, order.Customer);
    }
}
