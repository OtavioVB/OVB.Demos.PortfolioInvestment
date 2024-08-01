using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct QuantityAvailableValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private decimal? QuantityAvailable { get; }

    private QuantityAvailableValueObject(MethodResult<INotification> methodResult, decimal? quantityAvailable = null)
    {
        MethodResult = methodResult;
        QuantityAvailable = quantityAvailable;
    }

    private const string QUANTITY_AVAILABLE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE = "QUANTITY_AVAILABLE_CANNOT_BE_LESS_THAN_ZERO";
    private const string QUANTITY_AVAILABLE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE = "A quantidade disponível do ativo financeiro deve ser maior que zero.";

    public const int MINIMUM_VALUE = 0;

    public static QuantityAvailableValueObject Factory(decimal quantityAvailable)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (quantityAvailable < MINIMUM_VALUE)
            notifications.Add(Notification.FactoryFailure(
                code: QUANTITY_AVAILABLE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE,
                message: QUANTITY_AVAILABLE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new QuantityAvailableValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new QuantityAvailableValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            quantityAvailable: quantityAvailable);
    }

    public decimal GetQuantityAvailable()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);
        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(QuantityAvailable)!.Value;
    }

    public static implicit operator decimal(QuantityAvailableValueObject obj)
        => obj.GetQuantityAvailable();
    public static implicit operator QuantityAvailableValueObject(decimal obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(QuantityAvailableValueObject obj)
        => obj.MethodResult;
}
