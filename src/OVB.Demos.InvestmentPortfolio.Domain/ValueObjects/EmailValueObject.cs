using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;
using System.Net.Mail;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct EmailValueObject
{
    private string? Email { get; }
    public MethodResult<INotification> MethodResult { get; }

    private EmailValueObject(MethodResult<INotification> methodResult, string? email = null)
    {
        Email = email;
        MethodResult = methodResult;
    }

    private const string EMAIL_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_CODE = "EMAIL_CANNOT_BE_EMPTY_OR_WHITESPACE";
    private const string EMAIL_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_MESSAGE = "O email não pode ser vazio ou conter apenas espaços em branco.";

    private const string EMAIL_LENGTH_CANNOT_BE_GREATHER_THEN_MAXIMUM_ALLOWED_NOTIFICATION_CODE = "EMAIL_LENGTH_CANNOT_BE_GREATHER_THEN_MAXIMUM_ALLOWED";
    private static string EMAIL_LENGTH_CANNOT_BE_GREATHER_THEN_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE => $"O email não pode conter mais que {MAX_LENGTH} caracteres.";

    private const string EMAIL_MUST_BE_VALID_NOTIFICATION_CODE = "EMAIL_MUST_BE_VALID";
    private const string EMAIL_MUST_BE_VALID_NOTIFICATION_MESSAGE = "O email deve ser válido.";

    public const int MAX_LENGTH = 255;

    public static EmailValueObject Factory(string email)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 3;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (string.IsNullOrWhiteSpace(email))
            notifications.Add(Notification.FactoryFailure(
                code: EMAIL_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_CODE,
                message: EMAIL_CANNOT_BE_EMPTY_OR_WHITESPACE_NOTIFICATION_MESSAGE));

        if (email.Length > MAX_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: EMAIL_LENGTH_CANNOT_BE_GREATHER_THEN_MAXIMUM_ALLOWED_NOTIFICATION_CODE,
                message: EMAIL_LENGTH_CANNOT_BE_GREATHER_THEN_MAXIMUM_ALLOWED_NOTIFICATION_MESSAGE));

        try
        {
            var mailAddress = new MailAddress(address: email);
        }
        catch
        {
            notifications.Add(
                item: Notification.FactoryFailure(
                    code: EMAIL_MUST_BE_VALID_NOTIFICATION_CODE,
                    message: EMAIL_MUST_BE_VALID_NOTIFICATION_MESSAGE));
        }

        if (Notification.HasAnyNotifications(notifications))
            return new EmailValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new EmailValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            email: email);
    }

    public string GetEmail()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Email);
    }

    public static implicit operator MethodResult<INotification>(EmailValueObject obj)
        => obj.MethodResult;
    public static implicit operator string(EmailValueObject obj)
        => obj.GetEmail();
    public static implicit operator EmailValueObject(string obj)
        => Factory(obj);

}
