using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct GrantTypeValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private string? GrantType { get; }

    private GrantTypeValueObject(MethodResult<INotification> methodResult, string? grantType = null)
    {
        MethodResult = methodResult;
        GrantType = grantType;
    }

    public readonly static string[] ALLOWED_GRANT_TYPES = { "password" };

    private const string GRANT_TYPE_MUST_BE_VALID_NOTIFICATION_CODE = "GRANT_TYPE_MUST_BE_VALID";
    private const string GRANT_TYPE_MUST_BE_VALID_NOTIFICATION_MESSAGE = "O valor de concessão da autorização deve ser um válido ('password').";

    public static GrantTypeValueObject Factory(string grantType)
    {
        var isValidGrantType = false;

        foreach (var allowedGrantType in ALLOWED_GRANT_TYPES)
            if (allowedGrantType == grantType)
            {
                isValidGrantType = true;
                break;
            }

        if (!isValidGrantType)
            return new GrantTypeValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: [Notification.FactoryFailure(
                        code: GRANT_TYPE_MUST_BE_VALID_NOTIFICATION_CODE,
                        message: GRANT_TYPE_MUST_BE_VALID_NOTIFICATION_MESSAGE)]));

        return new GrantTypeValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            grantType: grantType);
    }

    public string GetGrantType()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(GrantType);
    }

    public static implicit operator GrantTypeValueObject(string obj)
        => Factory(obj);
    public static implicit operator string(GrantTypeValueObject obj)
        => obj.GetGrantType();
    public static implicit operator MethodResult<INotification>(GrantTypeValueObject obj)
        => obj.MethodResult;
}
