using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct OffsetValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private int? Offset { get; }

    private OffsetValueObject(MethodResult<INotification> methodResult, int? offset = null)
    {
        MethodResult = methodResult;
        Offset = offset;
    }

    public const int MAX_OFFSET = 50;
    public const int MIN_OFFSET = 5;

    private const string OFFSET_VALUE_CANNOT_BE_LESS_THAN_THE_MINIMUM_VALUE_ALLOWED_NOTIFICATION_CODE = "OFFSET_VALUE_CANNOT_BE_LESS_THAN_THE_MINIMUM_VALUE_ALLOWED";
    private const string OFFSET_VALUE_CANNOT_BE_LESS_THAN_THE_MINIMUM_VALUE_ALLOWED_NOTIFICATION_MESSAGE = "O número de itens a ser paginados não pode ser menor que número mínimo de 5 iten(s).";

    private const string OFFSET_VALUE_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_VALUE_ALLOWED_NOTIFICATION_CODE = "OFFSET_VALUE_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_VALUE_ALLOWED";
    private const string OFFSET_VALUE_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_VALUE_ALLOWED_NOTIFICATION_MESSAGE = "O número de itens a ser paginados não pode ser maior que número máximo de 50 iten(s).";

    public static OffsetValueObject Factory(int offset = 25)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (offset < MIN_OFFSET)
            notifications.Add(Notification.FactoryFailure(
                code: OFFSET_VALUE_CANNOT_BE_LESS_THAN_THE_MINIMUM_VALUE_ALLOWED_NOTIFICATION_CODE,
                message: OFFSET_VALUE_CANNOT_BE_LESS_THAN_THE_MINIMUM_VALUE_ALLOWED_NOTIFICATION_MESSAGE));

        if (offset > MAX_OFFSET)
            notifications.Add(Notification.FactoryFailure(
                code: OFFSET_VALUE_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_VALUE_ALLOWED_NOTIFICATION_CODE,
                message: OFFSET_VALUE_CANNOT_BE_GREATER_THAN_THE_MAXIMUM_VALUE_ALLOWED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new OffsetValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new OffsetValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            offset: offset);
    }

    public int GetOffset()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Offset)!.Value;
    }

    public static implicit operator MethodResult<INotification>(OffsetValueObject obj)
        => obj.MethodResult;
    public static implicit operator int(OffsetValueObject obj)
        => obj.GetOffset();
    public static implicit operator OffsetValueObject(int obj)
        => Factory(obj);
}
