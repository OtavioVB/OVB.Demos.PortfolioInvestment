using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class QuantityAvailableValueObjectValidationTests
{
    [Theory]
    [InlineData(4.234958)]
    [InlineData(5.508)]
    [InlineData(0.0587)]
    public void Quantity_Available_Value_Object_Should_Be_Valid(decimal quantityAvailable)
    {
        // Arrange

        // Act
        var quantity = QuantityAvailableValueObject.Factory(quantityAvailable);

        // Assert
        Assert.True(quantity.MethodResult.IsSuccess);
        Assert.Equal(quantityAvailable, quantity.GetQuantityAvailable());
        Assert.Equal<decimal>(quantityAvailable, quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.5)]
    [InlineData(-0.00324795)]
    public void Quantity_Available_Value_Object_Should_Be_Not_Valid(decimal quantityAvailable)
    {
        // Arrange

        // Act
        var quantity = QuantityAvailableValueObject.Factory(quantityAvailable);

        // Assert
        Assert.False(quantity.MethodResult.IsSuccess);
        Assert.NotEmpty(quantity.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => quantity.GetQuantityAvailable());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.5)]
    [InlineData(-0.00324795)]
    public void Quantity_Available_Value_Object_Should_Be_Send_Notification_Error_When_Quantity_Is_Less_Than_The_Minimum_Allowed(decimal quantityAvailable)
    {
        // Arrange
        const int EXPECTED_QUANTITY_MIN_VALUE = 0;
        const string EXPECTED_CODE = "QUANTITY_AVAILABLE_CANNOT_BE_LESS_THAN_ZERO";
        const string EXPECTED_MESSAGE = "A quantidade disponível do ativo financeiro deve ser maior que zero.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var quantity = QuantityAvailableValueObject.Factory(quantityAvailable);

        // Assert
        Assert.Single(quantity.MethodResult.Notifications);
        Assert.Equal(EXPECTED_QUANTITY_MIN_VALUE, QuantityAvailableValueObject.MINIMUM_VALUE);
        Assert.Equal(EXPECTED_CODE, quantity.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, quantity.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, quantity.MethodResult.Notifications[0].Type);
    }
}
