using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OperatorContext;

public sealed class OAuthOperatorAuthenticationServiceInputValidationTests
{
    [Fact]
    public void OAuthOperatorAuthenticationServiceInput_Should_Be_Valid()
    {
        // Arrange
        const string EXPECTED_GRANT_TYPE = "password";
        const string EXPECTED_EMAIL = "otaviovb.developer@gmail.com";
        const string EXPECTED_PASSWORD = "994jF%9adh$%";

        GrantTypeValueObject grantType = EXPECTED_GRANT_TYPE;
        PasswordValueObject password = PasswordValueObject.Factory(EXPECTED_PASSWORD);
        EmailValueObject email = EXPECTED_EMAIL;

        // Act
        var methodResult = MethodResult<INotification>.Factory(grantType, email, password);

        var input = OAuthOperatorAuthenticationServiceInput.Factory(
            grantType: grantType,
            email: email,
            password: password);

        // Assert
        Assert.Equal(EXPECTED_GRANT_TYPE, input.GrantType.GetGrantType());
        Assert.Equal(EXPECTED_EMAIL, input.Email.GetEmail());
        Assert.NotEmpty(input.Password.GetPasswordHashAndSalt(PasswordValueObjectValidationTests.PRIVATE_KEY).PasswordHash);
        Assert.Equal(methodResult.Notifications, input.GetInputValidationResult().Notifications);
        Assert.Equal(methodResult.IsSuccess, input.GetInputValidationResult().IsSuccess);
    }
}
