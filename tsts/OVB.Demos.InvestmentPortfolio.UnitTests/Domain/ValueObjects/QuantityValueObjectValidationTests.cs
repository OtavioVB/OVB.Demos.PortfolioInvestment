using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class QuantityValueObjectValidationTests
{
    [Theory]
    [InlineData(4.234958)]
    [InlineData(5.508)]
    [InlineData(0.0587)]
    public void Quantity_Value_Object_Should_Be_Valid(decimal quantity)
    {
        // Arrange

        // Act
        var quantityVo = QuantityValueObject.Factory(quantity);

        // Assert
        Assert.True(quantityVo.MethodResult.IsSuccess);
        Assert.Equal(quantity, quantityVo.GetQuantity());
        Assert.Equal<decimal>(quantity, quantityVo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.5)]
    [InlineData(-0.00324795)]
    public void Quantity_Value_Object_Should_Be_Not_Valid(decimal quantity)
    {
        // Arrange

        // Act
        var quantityVo = QuantityValueObject.Factory(quantity);

        // Assert
        Assert.False(quantityVo.MethodResult.IsSuccess);
        Assert.NotEmpty(quantityVo.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => quantityVo.GetQuantity());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.5)]
    [InlineData(-0.00324795)]
    public void Quantity_Value_Object_Should_Be_Send_Notification_Error_When_Quantity_Is_Less_Than_The_Minimum_Allowed(decimal quantity)
    {
        // Arrange
        const int EXPECTED_QUANTITY_MIN_VALUE = 0;
        const string EXPECTED_CODE = "QUANTITY_CANNOT_BE_LESS_THAN_ZERO";
        const string EXPECTED_MESSAGE = "A quantidade do ativo financeiro deve ser maior que zero.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var quantityVo = QuantityValueObject.Factory(quantity);

        // Assert
        Assert.Single(quantityVo.MethodResult.Notifications);
        Assert.Equal(EXPECTED_QUANTITY_MIN_VALUE, QuantityValueObject.MINIMUM_VALUE);
        Assert.Equal(EXPECTED_CODE, quantityVo.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, quantityVo.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, quantityVo.MethodResult.Notifications[0].Type);
    }
}
