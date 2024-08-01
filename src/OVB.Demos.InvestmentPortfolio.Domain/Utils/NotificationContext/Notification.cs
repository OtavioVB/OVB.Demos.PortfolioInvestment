using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;

public class Notification : INotification
{
    public string Code { get; }
    public string Message { get; }
    public string Type { get; }

    private Notification(string code, string message, string type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static INotification Factory(string code, string message, TypeNotification type)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentNullException(
                paramName: nameof(code));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(
                paramName: nameof(message));

        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(
                paramName: nameof(type));

        return new Notification(
            code: code,
            message: message,
            type: type.ToString());
    }

    public static INotification FactoryFailure(string code, string message)
        => Factory(code, message, TypeNotification.Failure);
    public static INotification FactorySuccess(string code, string message)
        => Factory(code, message, TypeNotification.Success);
    public static INotification FactoryInformation(string code, string message)
        => Factory(code, message, TypeNotification.Information);
    public static bool HasAnyNotifications<T>(List<T> notifications)
        => notifications.Count > 0;

}

