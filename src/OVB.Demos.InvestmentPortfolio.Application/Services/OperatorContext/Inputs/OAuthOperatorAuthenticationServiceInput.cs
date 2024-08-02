using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;

public readonly struct OAuthOperatorAuthenticationServiceInput
{
    public GrantTypeValueObject GrantType { get; }
    public EmailValueObject Email { get; }
    public PasswordValueObject Password { get; }

    private OAuthOperatorAuthenticationServiceInput(GrantTypeValueObject grantType, EmailValueObject email, PasswordValueObject password)
    {
        GrantType = grantType;
        Email = email;
        Password = password;
    }

    public static OAuthOperatorAuthenticationServiceInput Factory(GrantTypeValueObject grantType, EmailValueObject email, PasswordValueObject password)
        => new(grantType, email, password);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(GrantType, Email, Password);
}
