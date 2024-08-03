using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.CustomerContext;

public sealed class OAuthCustomerAuthenticationServiceInputValidationTests
{
    [Fact]
    public void OAuthCustomerAuthenticationServiceInput_Should_Be_Valid()
    {
        // Arrange
        const string EXPECTED_GRANT_TYPE = "password";
        const string EXPECTED_EMAIL = "otaviovb.developer@gmail.com";
        const string EXPECTED_PASSWORD = "994jF%9adh$%";

        GrantTypeValueObject grantType = EXPECTED_GRANT_TYPE;
        EmailValueObject email = EXPECTED_EMAIL;
        PasswordValueObject password = PasswordValueObject.Factory(EXPECTED_PASSWORD);

        // Act
        var methodResult = MethodResult<INotification>.Factory(email, password, grantType);

        var input = OAuthCustomerAuthenticationServiceInput.Factory(
            email: email,
            password: password,
            grantType: grantType);

        // Assert
        Assert.Equal(EXPECTED_GRANT_TYPE, input.GrantType);
        Assert.Equal(EXPECTED_EMAIL, input.Email);
        Assert.NotEmpty(input.Password.GetPasswordHashAndSalt(
            privateKey: PasswordValueObjectValidationTests.PRIVATE_KEY).PasswordHash);
        Assert.Equal(methodResult.Notifications, input.GetInputValidationResult().Notifications);
        Assert.Equal(methodResult.IsSuccess, input.GetInputValidationResult().IsSuccess);
    }
}
