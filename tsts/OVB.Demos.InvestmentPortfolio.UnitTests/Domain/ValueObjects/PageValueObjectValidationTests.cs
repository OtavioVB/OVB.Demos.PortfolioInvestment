using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class PageValueObjectValidationTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(20)]
    public void Page_Value_Object_Should_Be_Valid(int page)
    {
        // Arrange
        int EXPECTED_INDEX = page - 1;

        // Act
        var pageValueObject = PageValueObject.Factory(page);

        // Assert
        Assert.True(pageValueObject.MethodResult.IsSuccess);
        Assert.Empty(pageValueObject.MethodResult.Notifications);
        Assert.Equal<int>(page, pageValueObject);
        Assert.Equal(page, pageValueObject.GetPage());
        Assert.Equal(EXPECTED_INDEX, pageValueObject.GetIndex());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Page_Value_Object_Should_Be_Not_Valid(int page)
    {
        // Arrange

        // Act
        var pageValueObject = PageValueObject.Factory(page);

        // Assert
        Assert.False(pageValueObject.MethodResult.IsSuccess);
        Assert.NotEmpty(pageValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => (int)pageValueObject);
        Assert.Throws<BusinessValueObjectException>(() => pageValueObject.GetIndex());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Page_Value_Object_Should_Send_Notification_Error_As_Expected_When_Page_Is_Less_Than_The_Minimum_Value(int page)
    {
        // Arrange
        const int EXPECTED_MINIMUM_PAGE_VALUE = 1;

        const string EXPECTED_NOTIFICATION_CODE = "PAGE_LENGTH_CANNOT_BE_LESS_THAN_THE_MIN_VALUE_ALLOWED";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A página não pode ser menor que um.";
        const string EXPECTED_NOTIFICATION_TYPE = "Failure";

        // Act
        var pageValueObject = PageValueObject.Factory(page);

        // Assert
        Assert.Equal(EXPECTED_MINIMUM_PAGE_VALUE, PageValueObject.PAGE_MIN_VALUE);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, pageValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, pageValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, pageValueObject.MethodResult.Notifications[0].Type);
    }
}
