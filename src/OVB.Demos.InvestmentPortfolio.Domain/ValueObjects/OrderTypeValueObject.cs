using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct OrderTypeValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private OrderType? OrderType { get; }

    private OrderTypeValueObject(MethodResult<INotification> methodResult, OrderType? orderType = null)
    {
        MethodResult = methodResult;
        OrderType = orderType;
    }

    private const string ORDER_TYPE_IS_NOT_DEFINED_NOTIFICATION_CODE = "ORDER_TYPE_IS_NOT_DEFINED";
    private const string ORDER_TYPE_IS_NOT_DEFINED_NOTIFICATION_MESSAGE = "O tipo de ordem associado não é um suportado pelo enumerador da API.";

    public static OrderTypeValueObject Factory(string order)
    {
        var isPossibleToConvertAsOrderTypeEnumerator = Enum.TryParse<OrderType>(
            value: order,
            ignoreCase: false,
            result: out var orderType);

        if (!isPossibleToConvertAsOrderTypeEnumerator)
            return new OrderTypeValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: [Notification.FactoryFailure(
                        code: ORDER_TYPE_IS_NOT_DEFINED_NOTIFICATION_CODE,
                        message: ORDER_TYPE_IS_NOT_DEFINED_NOTIFICATION_MESSAGE)]));

        return new OrderTypeValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            orderType: orderType);
    }

    public OrderType GetOrderType()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(OrderType)!.Value;
    }

    public string GetOrderTypeAsString()
        => GetOrderType().ToString();

    public static implicit operator OrderType(OrderTypeValueObject obj)
        => obj.GetOrderType();
    public static implicit operator string(OrderTypeValueObject obj)
        => obj.GetOrderTypeAsString();
    public static implicit operator MethodResult<INotification>(OrderTypeValueObject obj)
        => obj.MethodResult;
    public static implicit operator OrderTypeValueObject(string obj)
        => Factory(obj);
    public static implicit operator OrderTypeValueObject(OrderType obj)
        => Factory(obj.ToString());
}
