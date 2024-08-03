using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Outputs;

public readonly struct OAuthCustomerAuthenticationServiceOutput
{
    public string TokenType { get; }
    public string AccessToken { get; }
    public GrantTypeValueObject GrantType { get; }
    public int ExpiresIn { get; }

    private OAuthCustomerAuthenticationServiceOutput(string tokenType, string accessToken, GrantTypeValueObject grantType, int expiresIn)
    {
        TokenType = tokenType;
        AccessToken = accessToken;
        GrantType = grantType;
        ExpiresIn = expiresIn;
    }

    public static OAuthCustomerAuthenticationServiceOutput Factory(string tokenType, string accessToken, GrantTypeValueObject grantType, int expiresIn)
        => new(tokenType, accessToken, grantType, expiresIn);
}
