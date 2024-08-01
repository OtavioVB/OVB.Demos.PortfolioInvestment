using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class UnitaryPriceValueObjectValidationTests
{
    [Theory]
    [InlineData(53.23)]
    [InlineData(0.082)]
    [InlineData(1.25)]
    public void Unitary_Price_Value_Object_Should_Be_Valid(decimal unitaryPrice)
    {
        // Arrange

        // Act
        var unitaryPriceValueObject = UnitaryPriceValueObject.Factory(unitaryPrice);

        // Assert
        Assert.True(unitaryPriceValueObject.MethodResult.IsSuccess);
        Assert.Empty(unitaryPriceValueObject.MethodResult.Notifications);
        Assert.Equal(unitaryPrice, unitaryPriceValueObject.GetUnitaryPrice());
        Assert.Equal<decimal>(unitaryPrice, unitaryPriceValueObject);
    }

    [Theory]
    [InlineData(-53.23)]
    [InlineData(-0.082)]
    [InlineData(0)]
    public void Unitary_Price_Value_Object_Should_Be_Not_Valid(decimal unitaryPrice)
    {
        // Arrange

        // Act
        var unitaryPriceValueObject = UnitaryPriceValueObject.Factory(unitaryPrice);

        // Assert
        Assert.False(unitaryPriceValueObject.MethodResult.IsSuccess);
        Assert.NotEmpty(unitaryPriceValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => unitaryPriceValueObject.GetUnitaryPrice());
    }


    [Theory]
    [InlineData(-53.23)]
    [InlineData(-0.082)]
    [InlineData(0)]
    public void Unitary_Price_Value_Object_Should_Be_Send_Notification_Error_When_The_Unitary_Price_Is_Less_Or_Equal_The_Minimum_Pric(decimal unitaryPrice)
    {
        // Arrange
        const int EXPECTED_MINIMUM_VALUE = 0;
        const string EXPECTED_CODE = "UNITARY_PRICE_CANNOT_BE_LESS_THAN_ZERO";
        const string EXPECTED_MESSAGE = "O valor unitário do ativo financeiro deve ser maior que zero.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var unitaryPriceValueObject = UnitaryPriceValueObject.Factory(unitaryPrice);

        // Assert
        Assert.Single(unitaryPriceValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_MINIMUM_VALUE, UnitaryPriceValueObject.MINIMUM_VALUE);
        Assert.Equal(EXPECTED_CODE, unitaryPriceValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, unitaryPriceValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, unitaryPriceValueObject.MethodResult.Notifications[0].Type);
    }
}