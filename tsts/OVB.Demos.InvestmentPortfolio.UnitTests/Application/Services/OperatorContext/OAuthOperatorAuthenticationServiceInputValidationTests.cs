using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;
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

        // Act
        var input = OAuthOperatorAuthenticationServiceInput.Factory(
            grantType: EXPECTED_GRANT_TYPE,
            email: EXPECTED_EMAIL,
            password: PasswordValueObject.Factory(EXPECTED_PASSWORD));

        // Assert
        Assert.Equal(EXPECTED_GRANT_TYPE, input.GrantType.GetGrantType());
        Assert.Equal(EXPECTED_EMAIL, input.Email.GetEmail());
        Assert.NotEmpty(input.Password.GetPasswordHashAndSalt(PasswordValueObjectValidationTests.PRIVATE_KEY).PasswordHash);
    }
}
