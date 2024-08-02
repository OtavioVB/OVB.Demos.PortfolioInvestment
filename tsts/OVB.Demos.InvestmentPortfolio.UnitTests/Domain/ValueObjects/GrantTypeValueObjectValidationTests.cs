using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class GrantTypeValueObjectValidationTests
{
    [Theory]
    [InlineData("password")]
    public void Grant_Type_Value_Object_Should_Be_Valid(string grantType)
    {
        // Arrange

        // Act
        var grantTypeValueObject = GrantTypeValueObject.Factory(
            grantType: grantType);

        // Assert
        Assert.True(grantTypeValueObject.MethodResult.IsSuccess);
        Assert.Empty(grantTypeValueObject.MethodResult.Notifications);
        Assert.Equal(grantType, grantTypeValueObject.GetGrantType());
        Assert.Equal(grantType, grantTypeValueObject);
    }

    [Theory]
    [InlineData("client_credentials")]
    [InlineData("authorization")]
    public void Grant_Type_Value_Object_Should_Be_Not_Valid(string grantType)
    {
        // Arrange

        // Act
        var grantTypeValueObject = GrantTypeValueObject.Factory(
            grantType: grantType);

        // Assert
        Assert.False(grantTypeValueObject.MethodResult.IsSuccess);
        Assert.NotEmpty(grantTypeValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(() => grantTypeValueObject.GetGrantType());
    }

    [Theory]
    [InlineData("client_credentials")]
    [InlineData("authorization")]
    public void Grant_Type_Value_Object_Should_Be_Send_Notification_Error_As_Expected_When_Grant_Type_Is_Not_Valid(string grantType)
    {
        // Arrange
        const string EXPECTED_CODE = "GRANT_TYPE_MUST_BE_VALID";
        const string EXPECTED_MESSAGE = "O valor de concessão da autorização deve ser um válido ('password').";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var grantTypeValueObject = GrantTypeValueObject.Factory(
            grantType: grantType);

        // Assert
        Assert.Single(grantTypeValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, grantTypeValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, grantTypeValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, grantTypeValueObject.MethodResult.Notifications[0].Type);
    }
}
