using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct AssetTypeValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private FinancialAssetType? Index { get; }

    private AssetTypeValueObject(MethodResult<INotification> methodResult, FinancialAssetType? type = null)
    {
        MethodResult = methodResult;
        Index = type;
    }

    private const string ASSET_TYPE_IS_NOT_DEFINED_NOTIFICATION_CODE = "ASSET_TYPE_IS_NOT_DEFINED";
    private const string ASSET_TYPE_IS_NOT_DEFINED_NOTIFICATION_MESSAGE = "O tipo do ativo financeiro não é suportado pela API.";

    public static AssetTypeValueObject Factory(string type)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        var isPossibleToConvert = Enum.TryParse<FinancialAssetType>(
            value: type,
            ignoreCase: false,
            result: out var typeConverted);

        if (!isPossibleToConvert)
            notifications.Add(Notification.FactoryFailure(
                code: ASSET_TYPE_IS_NOT_DEFINED_NOTIFICATION_CODE,
                message: ASSET_TYPE_IS_NOT_DEFINED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new AssetTypeValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new AssetTypeValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            type: typeConverted);
    }

    public FinancialAssetType GetType()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Index)!.Value;
    }

    public string GetTypeAsString()
        => GetType().ToString();

    public static implicit operator AssetTypeValueObject(FinancialAssetType obj)
        => Factory(obj.ToString());
    public static implicit operator AssetTypeValueObject(string obj)
        => Factory(obj);
    public static implicit operator string(AssetTypeValueObject obj)
        => obj.GetTypeAsString();
    public static implicit operator FinancialAssetType(AssetTypeValueObject obj)
        => obj.GetType();
    public static implicit operator MethodResult<INotification>(AssetTypeValueObject obj)
        => obj.MethodResult;
}
