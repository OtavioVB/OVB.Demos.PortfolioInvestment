using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class TotalPriceValueObjectValidationTests
{
    [Theory]
    [InlineData(53.23)]
    [InlineData(0.082)]
    [InlineData(1.25)]
    public void Total_Price_Value_Object_Should_Be_Valid(decimal totalPrice)
    {
        // Arrange

        // Act
        var totalPriceValueObject = TotalPriceValueObject.Factory(totalPrice);

        // Assert
        Assert.True(totalPriceValueObject.MethodResult.IsSuccess);
        Assert.Empty(totalPriceValueObject.MethodResult.Notifications);
        Assert.Equal(totalPrice, totalPriceValueObject.GetTotalPrice());
        Assert.Equal<decimal>(totalPrice, totalPriceValueObject);
    }

    [Theory]
    [InlineData(-53.23)]
    [InlineData(-0.082)]
    [InlineData(0)]
    public void Total_Price_Value_Object_Should_Be_Not_Valid(decimal totalPrice)
    {
        // Arrange

        // Act
        var totalPriceValueObject = TotalPriceValueObject.Factory(totalPrice);

        // Assert
        Assert.False(totalPriceValueObject.MethodResult.IsSuccess);
        Assert.NotEmpty(totalPriceValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => totalPriceValueObject.GetTotalPrice());
    }


    [Theory]
    [InlineData(-53.23)]
    [InlineData(-0.082)]
    [InlineData(0)]
    public void Total_Price_Value_Object_Should_Be_Send_Notification_Error_When_The_Total_Price_Is_Less_Or_Equal_The_Minimum_Pric(decimal totalPrice)
    {
        // Arrange
        const int EXPECTED_MINIMUM_VALUE = 0;
        const string EXPECTED_CODE = "TOTAL_PRICE_CANNOT_BE_LESS_THAN_ZERO";
        const string EXPECTED_MESSAGE = "O valor total do ativo financeiro deve ser maior que zero.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var totalPriceValueObject = TotalPriceValueObject.Factory(totalPrice);

        // Assert
        Assert.Single(totalPriceValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_MINIMUM_VALUE, TotalPriceValueObject.MINIMUM_VALUE);
        Assert.Equal(EXPECTED_CODE, totalPriceValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, totalPriceValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, totalPriceValueObject.MethodResult.Notifications[0].Type);
    }
}
