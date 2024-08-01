using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class OrderStatusValueObjectValidationTests
{
    [Theory]
    [InlineData("WAITING")]
    [InlineData("EXECUTED")]
    [InlineData("REJECTED")]
    public void Order_Status_Value_Object_Should_Be_Valid(string status)
    {
        // Arrange
        var statusEnumerator = Enum.Parse<OrderStatus>(status);

        // Act
        var orderStatusValueObject = OrderStatusValueObject.Factory(status);

        // Assert
        Assert.True(orderStatusValueObject.MethodResult.IsSuccess);
        Assert.Equal(status, orderStatusValueObject.GetOrderStatusAsString());
        Assert.Equal(status, orderStatusValueObject);
        Assert.Equal(statusEnumerator, orderStatusValueObject.GetOrderStatus());
        Assert.Equal<OrderStatus>(statusEnumerator, orderStatusValueObject);
    }

    [Theory]
    [InlineData("ERROR")]
    [InlineData("TESTE")]
    [InlineData("NOT_FOUND")]
    [InlineData("CANCELED")]
    [InlineData("CANCELLED")]
    public void Order_Status_Value_Object_Should_Be_Not_Valid(string status)
    {
        // Arrange

        // Act
        var orderStatusValueObject = OrderStatusValueObject.Factory(status);

        // Assert
        Assert.False(orderStatusValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(() => orderStatusValueObject.GetOrderStatusAsString());
        Assert.Throws<BusinessValueObjectException>(() => orderStatusValueObject.GetOrderStatus());
    }

    [Theory]
    [InlineData("ERROR")]
    [InlineData("TESTE")]
    [InlineData("NOT_FOUND")]
    [InlineData("CANCELED")]
    [InlineData("CANCELLED")]
    public void Order_Status_Value_Object_Should_Be_Not_Send_Error_Notification_As_Expected_When_The_Enum_Is_Not_Defined(string status)
    {
        // Arrange
        const string EXPECTED_CODE = "ORDER_STATUS_IS_NOT_DEFINED";
        const string EXPECTED_MESSAGE = "O status da ordem associada não é um suportado pelo enumerador da API.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var orderStatusValueObject = OrderStatusValueObject.Factory(status);

        // Assert
        Assert.Single(orderStatusValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, orderStatusValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, orderStatusValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, orderStatusValueObject.MethodResult.Notifications[0].Type);
    }
}
