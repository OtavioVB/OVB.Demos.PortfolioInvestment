using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Inputs;

public readonly struct OAuthCustomerAuthenticationServiceInput
{
    public EmailValueObject Email { get; }
    public PasswordValueObject Password { get; }
    public GrantTypeValueObject GrantType { get; }

    private OAuthCustomerAuthenticationServiceInput(EmailValueObject email, PasswordValueObject password, GrantTypeValueObject grantType)
    {
        Email = email;
        Password = password;
        GrantType = grantType;
    }

    public static OAuthCustomerAuthenticationServiceInput Factory(EmailValueObject email, PasswordValueObject password, GrantTypeValueObject grantType)
        => new(email, password, grantType);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(Email, Password, GrantType);
}
