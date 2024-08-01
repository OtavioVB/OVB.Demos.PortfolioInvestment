using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct CodeValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private string? Code { get; }

    private CodeValueObject(MethodResult<INotification> methodResult, string? code = null)
    {
        MethodResult = methodResult;
        Code = code;
    }

    private const string CODE_LENGTH_NEED_TO_BE_LESS_THAN_MAX_LENGTH_NOTIFICATION_CODE = "CODE_LENGTH_NEED_TO_BE_LESS_THAN_MAX_LENGTH";
    private static string CODE_LENGTH_NEED_TO_BE_LESS_THAN_MAX_LENGTH_NOTIFICATION_MESSAGE => $"O código deve conter até {MAX_LENGTH} caracteres.";

    private const string CODE_MUST_BE_NOT_EMPTY_OR_WHITESPACE_NOTIFICATION_CODE = "CODE_MUST_BE_NOT_EMPTY_OR_WHITESPACE";
    private const string CODE_MUST_BE_NOT_EMPTY_OR_WHITESPACE_NOTIFICATION_MESSAGE = "O código não pode ser vazio ou conter apenas espaços em branco.";

    public const int MAX_LENGTH = 32;

    public static CodeValueObject Factory(string code)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (code.Length > MAX_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: CODE_LENGTH_NEED_TO_BE_LESS_THAN_MAX_LENGTH_NOTIFICATION_CODE,
                message: CODE_LENGTH_NEED_TO_BE_LESS_THAN_MAX_LENGTH_NOTIFICATION_MESSAGE));

        if (string.IsNullOrWhiteSpace(code))
            notifications.Add(Notification.FactoryFailure(
                code: CODE_MUST_BE_NOT_EMPTY_OR_WHITESPACE_NOTIFICATION_CODE,
                message: CODE_MUST_BE_NOT_EMPTY_OR_WHITESPACE_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new CodeValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new CodeValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            code: code);
    }

    public string GetCode()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Code);
    }

    public static implicit operator CodeValueObject(string obj)
        => Factory(obj);
    public static implicit operator string(CodeValueObject obj)
        => obj.GetCode();
    public static implicit operator MethodResult<INotification>(CodeValueObject obj)
        => obj.MethodResult;
}
