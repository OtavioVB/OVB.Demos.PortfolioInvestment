using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct PageValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private int? Page { get; }

    private PageValueObject(MethodResult<INotification> methodResult, int? page = null)
    {
        MethodResult = methodResult;
        Page = page;
    }

    public const int PAGE_MIN_VALUE = 1;

    private const string PAGE_LENGTH_CANNOT_BE_LESS_THAN_THE_MIN_VALUE_ALLOWED_NOTIFICATION_CODE = "PAGE_LENGTH_CANNOT_BE_LESS_THAN_THE_MIN_VALUE_ALLOWED";
    private const string PAGE_LENGTH_CANNOT_BE_LESS_THAN_THE_MIN_VALUE_ALLOWED_NOTIFICATION_MESSAGE = "A página não pode ser menor que um.";

    public static PageValueObject Factory(int page = PAGE_MIN_VALUE)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (page < PAGE_MIN_VALUE)
            notifications.Add(Notification.FactoryFailure(
                code: PAGE_LENGTH_CANNOT_BE_LESS_THAN_THE_MIN_VALUE_ALLOWED_NOTIFICATION_CODE,
                message: PAGE_LENGTH_CANNOT_BE_LESS_THAN_THE_MIN_VALUE_ALLOWED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new PageValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new PageValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            page: page);
    }

    public int GetPage()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Page)!.Value;
    }

    public int GetIndex()
        => GetPage() - 1;

    public static implicit operator int(PageValueObject obj)
        => obj.GetPage();
    public static implicit operator PageValueObject(int obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(PageValueObject obj)
        => obj.MethodResult;
}
