using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct QuantityValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private decimal? Quantity { get; }

    private QuantityValueObject(MethodResult<INotification> methodResult, decimal? quantity = null)
    {
        MethodResult = methodResult;
        Quantity = quantity;
    }

    private const string QUANTITY_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE = "QUANTITY_CANNOT_BE_LESS_THAN_ZERO";
    private const string QUANTITY_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE = "A quantidade do ativo financeiro deve ser maior que zero.";

    public const int MINIMUM_VALUE = 0;

    public static QuantityValueObject Factory(decimal quantity)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (quantity < MINIMUM_VALUE)
            notifications.Add(Notification.FactoryFailure(
                code: QUANTITY_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE,
                message: QUANTITY_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new QuantityValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new QuantityValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            quantity: quantity);
    }

    public decimal GetQuantity()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);
        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Quantity)!.Value;
    }

    public static implicit operator decimal(QuantityValueObject obj)
        => obj.GetQuantity();
    public static implicit operator QuantityValueObject(decimal obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(QuantityValueObject obj)
        => obj.MethodResult;
}