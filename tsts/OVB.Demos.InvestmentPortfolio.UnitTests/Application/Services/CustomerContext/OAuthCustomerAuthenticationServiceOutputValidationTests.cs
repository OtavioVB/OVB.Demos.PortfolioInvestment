using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Outputs;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.CustomerContext;

public sealed class OAuthCustomerAuthenticationServiceOutputValidationTests
{
    [Fact]
    public void OAuthCustomerAuthenticationServiceOutput_Should_Be_Valid_As_Expected()
    {
        // Arrange
        const string EXPECTED_TOKEN_TYPE = "Bearer";
        const string EXPECTED_ACCESS_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        const string EXPECTED_GRANT_TYPE = "password";
        const int EXPECTED_EXPIRES_IN = 3600;

        // Act
        var output = OAuthCustomerAuthenticationServiceOutput.Factory(
            tokenType: EXPECTED_TOKEN_TYPE,
            accessToken: EXPECTED_ACCESS_TOKEN,
            grantType: EXPECTED_GRANT_TYPE,
            expiresIn: EXPECTED_EXPIRES_IN);

        // Assert
        Assert.Equal(EXPECTED_TOKEN_TYPE, output.TokenType);
        Assert.Equal(EXPECTED_ACCESS_TOKEN, output.AccessToken);
        Assert.Equal(EXPECTED_GRANT_TYPE, output.GrantType);
        Assert.Equal(EXPECTED_EXPIRES_IN, output.ExpiresIn);
    }
}
