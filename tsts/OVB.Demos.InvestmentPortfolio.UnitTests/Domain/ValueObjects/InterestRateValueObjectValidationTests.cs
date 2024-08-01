using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class InterestRateValueObjectValidationTests
{
    [Theory]
    [InlineData(10.523)]
    [InlineData(1.25)]
    [InlineData(0.574)]
    [InlineData(0.0383)]
    public void Interest_Rate_Value_Object_Should_Be_Valid(decimal interestRate)
    {
        // Arrange

        // Act
        var interestRateValueObject = (InterestRateValueObject)interestRate;

        // Assert
        Assert.True(interestRateValueObject.MethodResult.IsSuccess);
        Assert.Equal(interestRate, interestRateValueObject.GetInterestRate());
        Assert.Equal<decimal>(interestRate, interestRateValueObject);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.0487)]
    [InlineData(-0.563)]
    public void Interest_Rate_Value_Object_Should_Be_Not_Valid(decimal interestRate)
    {
        // Arrange

        // Act
        var interestRateValueObject = (InterestRateValueObject)interestRate;

        // Assert
        Assert.False(interestRateValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(() => interestRateValueObject.GetInterestRate());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.0487)]
    [InlineData(-0.563)]
    public void Interest_Rate_Value_Object_Should_Be_Send_Notification_Error_When_The_Interest_Rate_Is_Less_Or_Equal_Than_The_Minimum_Value(decimal interestRate)
    {
        // Arrange
        const int EXPECTED_MINIMUM_VALUE = 0;
        const string EXPECTED_CODE = "INTEREST_RATE_CANNOT_BE_LESS_THAN_ZERO";
        const string EXPECTED_MESSAGE = "A taxa de juros deve ser maior que zero percentual.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var interestRateValueObject = (InterestRateValueObject)interestRate;

        // Assert
        Assert.Equal(EXPECTED_MINIMUM_VALUE, InterestRateValueObject.MINIMUM_VALUE);
        Assert.Equal(EXPECTED_CODE, interestRateValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, interestRateValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, interestRateValueObject.MethodResult.Notifications[0].Type);
    }
}
