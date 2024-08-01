using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct DescriptionValueObject
{
    private string? Description { get; }
    public MethodResult<INotification> MethodResult { get; }

    private DescriptionValueObject(string? description, MethodResult<INotification> methodResult)
    {
        Description = description;
        MethodResult = methodResult;
    }

    public const int MAX_LENGTH = 64;

    private const string DESCRIPTION_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_CODE = "DESCRIPTION_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED";
    private static string DESCRIPTION_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE => $"A descrição não pode conter mais que {MAX_LENGTH} caracteres";

    public static DescriptionValueObject Factory(
        string? description)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (description is null)
            return new DescriptionValueObject(
                description: description,
                methodResult: MethodResult<INotification>.FactorySuccess());

        if (description.Length > MAX_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: DESCRIPTION_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_CODE,
                message: DESCRIPTION_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new DescriptionValueObject(
                description: null,
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new DescriptionValueObject(
            description: description,
            methodResult: MethodResult<INotification>.FactorySuccess());
    }

    public string? GetDescription()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return Description;
    }

    public static implicit operator string?(DescriptionValueObject obj)
        => obj.GetDescription();
    public static implicit operator DescriptionValueObject(string? obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(DescriptionValueObject obj)
        => obj.MethodResult;
}
