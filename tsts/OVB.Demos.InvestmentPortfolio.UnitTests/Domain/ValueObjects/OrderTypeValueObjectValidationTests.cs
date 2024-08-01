using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class OrderTypeValueObjectValidationTests
{
    [Theory]
    [InlineData("SELL")]
    [InlineData("BUY")]
    public void Order_Type_Value_Object_Should_Be_Valid(string orderType)
    {
        // Arrange
        var EXPECTED_ORDER_TYPE_ENUMERATOR = Enum.Parse<OrderType>(orderType);

        // Act
        var orderTypeValueObject = OrderTypeValueObject.Factory(orderType);

        // Assert
        Assert.True(orderTypeValueObject.MethodResult.IsSuccess);
        Assert.Empty(orderTypeValueObject.MethodResult.Notifications);
        Assert.Equal(orderType, orderTypeValueObject.GetOrderTypeAsString());
        Assert.Equal(orderType, orderTypeValueObject);
        Assert.Equal(EXPECTED_ORDER_TYPE_ENUMERATOR, orderTypeValueObject.GetOrderType());
        Assert.Equal<OrderType>(EXPECTED_ORDER_TYPE_ENUMERATOR, orderTypeValueObject);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("LOAN")]
    [InlineData("TESTE")]
    [InlineData("ERROR")]
    public void Order_Type_Value_Object_Should_Be_Not_Valid(string orderType)
    {
        // Arrange

        // Act
        var orderTypeValueObject = OrderTypeValueObject.Factory(orderType);

        // Assert
        Assert.False(orderTypeValueObject.MethodResult.IsSuccess);
        Assert.Single(orderTypeValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => orderTypeValueObject.GetOrderType());
        Assert.Throws<BusinessValueObjectException>(() => orderTypeValueObject.GetOrderTypeAsString());
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("LOAN")]
    [InlineData("TESTE")]
    [InlineData("ERROR")]
    public void Order_Type_Value_Object_Should_Be_Send_Notification_Error_As_Expected_When_The_Order_Type_Is_Not_Defined(string orderType)
    {
        // Arrange
        const string EXPECTED_CODE = "ORDER_TYPE_IS_NOT_DEFINED";
        const string EXPECTED_MESSAGE = "O tipo de ordem associado não é um suportado pelo enumerador da API.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var orderTypeValueObject = OrderTypeValueObject.Factory(orderType);

        // Assert
        Assert.Equal(EXPECTED_CODE, orderTypeValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, orderTypeValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, orderTypeValueObject.MethodResult.Notifications[0].Type);
    }
}
