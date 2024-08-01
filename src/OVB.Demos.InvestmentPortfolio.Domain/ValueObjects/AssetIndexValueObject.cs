using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct AssetIndexValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private FinancialAssetIndex? Index { get; }

    private AssetIndexValueObject(MethodResult<INotification> methodResult, FinancialAssetIndex? index = null)
    {
        MethodResult = methodResult;
        Index = index;
    }

    private const string ASSET_INDEX_IS_NOT_DEFINED_NOTIFICATION_CODE = "ASSET_INDEX_IS_NOT_DEFINED";
    private const string ASSET_INDEX_IS_NOT_DEFINED_NOTIFICATION_MESSAGE = "O índice do ativo financeiro não é suportado pela API.";

    public static AssetIndexValueObject Factory(string index)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        var isPossibleToConvert = Enum.TryParse<FinancialAssetIndex>(
            value: index,
            ignoreCase: false,
            result: out var typeIndex);

        if (!isPossibleToConvert)
            notifications.Add(Notification.FactoryFailure(
                code: ASSET_INDEX_IS_NOT_DEFINED_NOTIFICATION_CODE,
                message: ASSET_INDEX_IS_NOT_DEFINED_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new AssetIndexValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new AssetIndexValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            index: typeIndex);
    }

    public FinancialAssetIndex GetIndex()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Index)!.Value;
    }

    public string GetIndexAsString()
        => GetIndex().ToString();

    public static implicit operator AssetIndexValueObject(FinancialAssetIndex obj)
        => Factory(obj.ToString());
    public static implicit operator AssetIndexValueObject(string obj)
        => Factory(obj);
    public static implicit operator string(AssetIndexValueObject obj)
        => obj.GetIndexAsString();
    public static implicit operator FinancialAssetIndex(AssetIndexValueObject obj)
        => obj.GetIndex();
    public static implicit operator MethodResult<INotification>(AssetIndexValueObject obj)
        => obj.MethodResult;
}
