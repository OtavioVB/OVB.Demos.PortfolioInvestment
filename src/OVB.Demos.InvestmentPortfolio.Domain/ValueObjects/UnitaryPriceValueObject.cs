using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct UnitaryPriceValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private decimal? UnitaryPrice { get; }

    private UnitaryPriceValueObject(MethodResult<INotification> methodResult, decimal? unitaryPrice = null)
    {
        MethodResult = methodResult;
        UnitaryPrice = unitaryPrice;
    }

    private const string UNITARY_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE = "UNITARY_PRICE_CANNOT_BE_LESS_THAN_ZERO";
    private const string UNITARY_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE = "O valor unitário do ativo financeiro deve ser maior que zero.";

    public const int MINIMUM_VALUE = 0;

    public static UnitaryPriceValueObject Factory(decimal unitaryPrice)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (unitaryPrice < MINIMUM_VALUE)
            notifications.Add(Notification.FactoryFailure(
                code: UNITARY_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE,
                message: UNITARY_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new UnitaryPriceValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new UnitaryPriceValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            unitaryPrice: unitaryPrice);
    }

    public decimal GetUnitaryPrice()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);
        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(UnitaryPrice)!.Value;
    }

    public static implicit operator decimal(UnitaryPriceValueObject obj)
        => obj.GetUnitaryPrice();
    public static implicit operator UnitaryPriceValueObject(decimal obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(UnitaryPriceValueObject obj)
        => obj.MethodResult;
}
