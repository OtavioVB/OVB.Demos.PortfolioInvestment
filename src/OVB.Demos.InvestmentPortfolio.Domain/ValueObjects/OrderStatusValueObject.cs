using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct OrderStatusValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private OrderStatus? OrderStatus { get; }

    private OrderStatusValueObject(MethodResult<INotification> methodResult, OrderStatus? orderStatus = null)
    {
        MethodResult = methodResult;
        OrderStatus = orderStatus;
    }

    private const string ORDER_STATUS_IS_NOT_DEFINED_NOTIFICATION_CODE = "ORDER_STATUS_IS_NOT_DEFINED";
    private const string ORDER_STATUS_IS_NOT_DEFINED_NOTIFICATION_MESSAGE = "O status da ordem associada não é um suportado pelo enumerador da API.";

    public static OrderStatusValueObject Factory(string extract)
    {
        var isPossibleToConvertAsOrderStatusEnumerator = Enum.TryParse<OrderStatus>(
            value: extract,
            ignoreCase: false,
            result: out var typeOrder);

        if (!isPossibleToConvertAsOrderStatusEnumerator)
            return new OrderStatusValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: [Notification.FactoryFailure(
                        code: ORDER_STATUS_IS_NOT_DEFINED_NOTIFICATION_CODE,
                        message: ORDER_STATUS_IS_NOT_DEFINED_NOTIFICATION_MESSAGE)]));

        return new OrderStatusValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            orderStatus: typeOrder);
    }

    public OrderStatus GetOrderStatus()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(OrderStatus)!.Value;
    }

    public string GetOrderStatusAsString()
        => GetOrderStatus().ToString();

    public static implicit operator OrderStatus(OrderStatusValueObject obj)
        => obj.GetOrderStatus();
    public static implicit operator string(OrderStatusValueObject obj)
        => obj.GetOrderStatusAsString();
    public static implicit operator MethodResult<INotification>(OrderStatusValueObject obj)
        => obj.MethodResult;
    public static implicit operator OrderStatusValueObject(string obj)
        => Factory(obj);
    public static implicit operator OrderStatusValueObject(OrderStatus obj)
        => Factory(obj.ToString());
}