using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct AssetSymbolValueObject
{
    private string? Symbol { get; }
    public MethodResult<INotification> MethodResult { get; }

    private AssetSymbolValueObject(MethodResult<INotification> methodResult, string? symbol = null)
    {
        Symbol = symbol;
        MethodResult = methodResult;
    }

    private const string ASSET_SYMBOL_CAN_NOT_BE_EMPTY_OR_WHITE_SPACE_NOTIFICATION_CODE = "ASSET_SYMBOL_CAN_NOT_BE_EMPTY_OR_WHITE_SPACE";
    private const string ASSET_SYMBOL_CAN_NOT_BE_EMPTY_OR_WHITE_SPACE_NOTIFICATION_MESSAGE = "O símbolo do ativo financeiro não pode ser vazio ou conter espaços em branco.";

    private const string ASSET_SYMBOL_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_NOTIFICATION_CODE = "ASSET_SYMBOL_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM";
    private static string ASSET_SYMBOL_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_NOTIFICATION_MESSAGE => $"O símbolo do ativo financeiro não pode conter mais que {MAX_LENGTH} caracteres.";

    public const int MAX_LENGTH = 32;

    public static AssetSymbolValueObject Factory(string symbol)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (string.IsNullOrWhiteSpace(symbol))
            notifications.Add(Notification.FactoryFailure(
                code: ASSET_SYMBOL_CAN_NOT_BE_EMPTY_OR_WHITE_SPACE_NOTIFICATION_CODE,
                message: ASSET_SYMBOL_CAN_NOT_BE_EMPTY_OR_WHITE_SPACE_NOTIFICATION_MESSAGE));

        if (symbol.Length > MAX_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: ASSET_SYMBOL_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_NOTIFICATION_CODE,
                message: ASSET_SYMBOL_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new AssetSymbolValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()),
                symbol: symbol);

        return new AssetSymbolValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            symbol: symbol);
    }

    public string GetSymbol()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Symbol);
    }

    public static implicit operator AssetSymbolValueObject(string obj)
        => Factory(obj);
    public static implicit operator string(AssetSymbolValueObject obj)
        => obj.GetSymbol();
    public static implicit operator MethodResult<INotification>(AssetSymbolValueObject obj)
        => obj.MethodResult;
}
