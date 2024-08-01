using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct TotalPriceValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private decimal? TotalPrice { get; }

    private TotalPriceValueObject(MethodResult<INotification> methodResult, decimal? totalPrice = null)
    {
        MethodResult = methodResult;
        TotalPrice = totalPrice;
    }

    private const string TOTAL_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE = "TOTAL_PRICE_CANNOT_BE_LESS_THAN_ZERO";
    private const string TOTAL_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE = "O valor total do ativo financeiro deve ser maior que zero.";

    public const int MINIMUM_VALUE = 0;

    public static TotalPriceValueObject Factory(decimal totalPrice)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (totalPrice <= MINIMUM_VALUE)
            notifications.Add(Notification.FactoryFailure(
                code: TOTAL_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE,
                message: TOTAL_PRICE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new TotalPriceValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new TotalPriceValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            totalPrice: totalPrice);
    }

    public decimal GetTotalPrice()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);
        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(TotalPrice)!.Value;
    }

    public static implicit operator decimal(TotalPriceValueObject obj)
        => obj.GetTotalPrice();
    public static implicit operator TotalPriceValueObject(decimal obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(TotalPriceValueObject obj)
        => obj.MethodResult;
}
