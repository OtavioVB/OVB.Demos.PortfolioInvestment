using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Outputs;

public readonly struct OAuthOperatorAuthenticationServiceOutput
{
    public string TokenType { get; }
    public string AccessToken { get; }
    public GrantTypeValueObject GrantType { get; }
    public int ExpiresIn { get; }

    private OAuthOperatorAuthenticationServiceOutput(string tokenType, string accessToken, GrantTypeValueObject grantType, int expiresIn)
    {
        TokenType = tokenType;
        AccessToken = accessToken;
        GrantType = grantType;
        ExpiresIn = expiresIn;
    }

    public static OAuthOperatorAuthenticationServiceOutput Factory(string tokenType, string accessToken, GrantTypeValueObject grantType, int expiresIn)
        => new(tokenType, accessToken, grantType, expiresIn);
}
