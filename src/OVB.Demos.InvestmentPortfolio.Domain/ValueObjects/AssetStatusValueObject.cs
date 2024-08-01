using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct AssetStatusValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private FinancialAssetStatus? Index { get; }

    private AssetStatusValueObject(MethodResult<INotification> methodResult, FinancialAssetStatus? type = null)
    {
        MethodResult = methodResult;
        Index = type;
    }

    private const string ASSET_STATUS_IS_NOT_DEFINED_NOTIFICATION_CODE = "ASSET_STATUS_IS_NOT_DEFINED";
    private const string ASSET_STATUS_IS_NOT_DEFINED_NOTIFICATION_MESSAGE = "O status do ativo financeiro não é suportado pela API.";

    public static AssetStatusValueObject Factory(string type)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        var isPossibleToConvert = Enum.TryParse<FinancialAssetStatus>(
            value: type,
            ignoreCase: false,
            result: out var typeConverted);

        if (!isPossibleToConvert)
            notifications.Add(Notification.FactoryFailure(
                code: ASSET_STATUS_IS_NOT_DEFINED_NOTIFICATION_CODE,
                message: ASSET_STATUS_IS_NOT_DEFINED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new AssetStatusValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new AssetStatusValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            type: typeConverted);
    }

    public FinancialAssetStatus GetStatus()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Index)!.Value;
    }

    public string GetStatusAsString()
        => GetStatus().ToString();

    public static implicit operator string(AssetStatusValueObject obj)
        => obj.GetStatusAsString();
    public static implicit operator FinancialAssetStatus(AssetStatusValueObject obj)
        => obj.GetStatus();
    public static implicit operator MethodResult<INotification>(AssetStatusValueObject obj)
        => obj.MethodResult;
}