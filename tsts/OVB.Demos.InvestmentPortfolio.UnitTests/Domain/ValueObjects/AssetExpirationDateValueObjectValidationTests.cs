using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class AssetExpirationDateValueObjectValidationTests
{
    [Fact]
    public void Asset_Expiration_Date_Should_Be_Valid()
    {
        // Arrange
        const int PLUS_DAYS_ON_FUTURE = 7;
        const string EXPIRATION_DATE_AS_STRING_FORMAT = "yyyy-MM-dd"; 
        
        var expirationDateFuture = DateTime.UtcNow.AddDays(PLUS_DAYS_ON_FUTURE);

        // Act
        var expirationDate = AssetExpirationDateValueObject.Factory(
            expirationDate: expirationDateFuture);

        // Assert
        Assert.True(expirationDate.MethodResult.IsSuccess);
        Assert.Equal(expirationDateFuture.ToString(EXPIRATION_DATE_AS_STRING_FORMAT), expirationDate.GetExpirationDateAsString());
        Assert.Equal(expirationDateFuture.Date, expirationDate.GetExpirationDate());
    }

    [Theory]
    [InlineData("2024-07-31")]
    [InlineData("2000-07-31")]
    [InlineData("2000-01-01")]
    public void Asset_Expiration_Date_Should_Be_Not_Valid(string date)
    {
        // Arrange
        var dateTest = DateTime.Parse(date);

        // Act
        var expirationDate = AssetExpirationDateValueObject.Factory(
            expirationDate: dateTest);

        // Assert
        Assert.False(expirationDate.MethodResult.IsSuccess);
        Assert.NotEmpty(expirationDate.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() =>
        {
            expirationDate.GetExpirationDate();
        });
        Assert.Throws<BusinessValueObjectException>(() =>
        {
            expirationDate.GetExpirationDateAsString();
        });
    }

    [Fact]
    public void Asset_Expiration_Date_Should_Invalid_Notification_Equal_As_Expected()
    {
        // Arrange
        const string EXPECTED_CODE = "ASSET_EXPIRATION_DATE_CANNOT_BE_LESS_THAN_NOW";
        const string EXPECTED_MESSAGE = "A data de vencimento do ativo financeiro não pode ser menor ou igual a data atual.";
        const string EXPECTED_TYPE = "Failure";

        var expirationDateFuture = DateTime.Parse("2000-12-05");

        // Act
        var expirationDate = AssetExpirationDateValueObject.Factory(
            expirationDate: expirationDateFuture);

        // Assert
        Assert.Single(expirationDate.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, expirationDate.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, expirationDate.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, expirationDate.MethodResult.Notifications[0].Type);
    }
}
