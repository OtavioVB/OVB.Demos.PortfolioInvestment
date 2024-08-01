using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct NameValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private string? Name { get; }

    private NameValueObject(MethodResult<INotification> methodResult, string? name = null)
    {
        MethodResult = methodResult;
        Name = name;
    }

    public const int MAX_LENGTH = 64;

    private const string NAME_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_CODE = "NAME_CANNOT_BE_EMPTY_OR_WHITESPACE";
    private const string NAME_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_MESSAGE = "O nome completo não pode ser vazio ou conter apenas espaços em branco.";

    private const string NAME_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_CODE = "NAME_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED";
    private static string NAME_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE => $"O nome completo deve conter até {MAX_LENGTH} caracteres.";

    public static NameValueObject Factory(string name)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (string.IsNullOrWhiteSpace(name))
            notifications.Add(Notification.FactoryFailure(
                code: NAME_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_CODE,
                message: NAME_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_MESSAGE));

        if (name.Length > MAX_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: NAME_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_CODE,
                message: NAME_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new NameValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new NameValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            name: name);
    }

    public string GetName()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Name);
    }

    public static implicit operator string(NameValueObject obj)
        => obj.GetName();
    public static implicit operator MethodResult<INotification>(NameValueObject obj)
        => obj.MethodResult;
    public static implicit operator NameValueObject(string obj)
        => Factory(obj);
}
