using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct InterestRateValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private decimal? InterestRate { get; }

    private InterestRateValueObject(MethodResult<INotification> methodResult, decimal? interestRate = null)
    {
        MethodResult = methodResult;
        InterestRate = interestRate;
    }

    private const string INTEREST_RATE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE = "INTEREST_RATE_CANNOT_BE_LESS_THAN_ZERO";
    private const string INTEREST_RATE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE = "A taxa de juros deve ser maior que zero percentual.";

    public const int MINIMUM_VALUE = 0;

    public static InterestRateValueObject Factory(decimal interestRate)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 1;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (interestRate < MINIMUM_VALUE)
            notifications.Add(Notification.FactoryFailure(
                code: INTEREST_RATE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_CODE,
                message: INTEREST_RATE_CANNOT_BE_LESS_THAN_ZERO_NOTIFICATION_MESSAGE));

        if (Notification.HasAnyNotifications(notifications))
            return new InterestRateValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new InterestRateValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            interestRate: interestRate);
    }

    public decimal GetInterestRate()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);
        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(InterestRate)!.Value;
    }

    public static implicit operator decimal(InterestRateValueObject obj)
        => obj.GetInterestRate();
    public static implicit operator InterestRateValueObject(decimal obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(InterestRateValueObject obj)
        => obj.MethodResult;
}
