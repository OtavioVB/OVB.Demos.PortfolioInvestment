using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OperatorContext.Sendloads;

public readonly struct OAuthOperatorAuthenticationSendloadOutput
{
    public string AccessToken { get; }
    public string TokenType { get; }
    public int ExpiresIn { get; }
    public string GrantType { get; }
    public INotification[] Notifications { get; }

    private OAuthOperatorAuthenticationSendloadOutput(string accessToken, string tokenType, int expiresIn, string grantType, INotification[] notifications)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        GrantType = grantType;
        Notifications = notifications;
    }

    public static OAuthOperatorAuthenticationSendloadOutput Factory(string accessToken, string tokenType, int expiresIn, string grantType, INotification[] notifications)
        => new(accessToken, tokenType, expiresIn, grantType, notifications);
}
