using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct AssetExpirationDateValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private DateTime? ExpirationDate { get; }

    private AssetExpirationDateValueObject(MethodResult<INotification> methodResult, DateTime? expirationDate = null)
    {
        MethodResult = methodResult;
        ExpirationDate = expirationDate;
    }

    private const string ASSET_EXPIRATION_DATE_CANNOT_BE_LESS_THAN_NOW_NOTIFICATION_CODE = "ASSET_EXPIRATION_DATE_CANNOT_BE_LESS_THAN_NOW";
    private const string ASSET_EXPIRATION_DATE_CANNOT_BE_LESS_THAN_NOW_NOTIFICATION_MESSAGE = "A data de vencimento do ativo financeiro não pode ser menor ou igual a data atual.";

    public static AssetExpirationDateValueObject Factory(DateTime expirationDate)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (expirationDate.Date <= DateTime.UtcNow.Date)
            notifications.Add(Notification.FactoryFailure(
                code: ASSET_EXPIRATION_DATE_CANNOT_BE_LESS_THAN_NOW_NOTIFICATION_CODE,
                message: ASSET_EXPIRATION_DATE_CANNOT_BE_LESS_THAN_NOW_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new AssetExpirationDateValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new AssetExpirationDateValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            expirationDate: expirationDate.Date);
    }

    public DateTime GetExpirationDate()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(ExpirationDate)!.Value;
    }

    private const string ASSET_EXPIRATION_DATE_STRING_FORMAT_EXPECTED = "yyyy-MM-dd";

    public string GetExpirationDateAsString()
        => GetExpirationDate().ToString(ASSET_EXPIRATION_DATE_STRING_FORMAT_EXPECTED);

    public static implicit operator AssetExpirationDateValueObject(DateTime obj)
        => Factory(obj);
    public static implicit operator DateTime(AssetExpirationDateValueObject obj)
        => obj.GetExpirationDate();
    public static implicit operator string(AssetExpirationDateValueObject obj)
        => obj.GetExpirationDateAsString();
    public static implicit operator MethodResult<INotification>(AssetExpirationDateValueObject obj)
        => obj.MethodResult;
}
