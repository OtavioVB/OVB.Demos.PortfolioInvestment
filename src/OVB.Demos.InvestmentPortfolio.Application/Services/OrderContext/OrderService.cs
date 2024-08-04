using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext;

public sealed class OrderService : IOrderService
{
    private readonly IBaseRepository<Order> _orderBaseRepository;
    private readonly IExtensionFinancialAssetRepository _extensionFinancialAssetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IBaseRepository<Order> orderBaseRepository, 
        IExtensionFinancialAssetRepository extensionFinancialAssetRepository,
        IUnitOfWork unitOfWork)
    {
        _orderBaseRepository = orderBaseRepository;
        _extensionFinancialAssetRepository = extensionFinancialAssetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MethodResult<INotification, CreateOrderServiceOutput>> CreateOrderServiceAsync(
        CreateOrderServiceInput input, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationtResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, CreateOrderServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        if (input.Type.GetOrderType() == OrderType.BUY)
        {
            var updateFinancialAssetQuantityAvailableAsync = await _extensionFinancialAssetRepository.UpdateFinancialAssetIfBuyProcessQuantityIsGreaterThanTheMinimumAsync(
                financialAssetId: input.FinancialAsset.Id,
                quantityWillBuy: input.Quantity,
                cancellationToken: cancellationToken);

            const string CREATE_ORDER_BUY_CANNOT_BE_DONE_BECAUSE_DOESNT_HAS_ASSET_QUANTITY_AVAILABLE_NOTIFICATION_CODE = "CREATE_ORDER_BUY_CANNOT_BE_DONE_BECAUSE_DOESNT_HAS_ASSET_QUANTITY_AVAILABLE";
            const string CREATE_ORDER_BUY_CANNOT_BE_DONE_BECAUSE_DOESNT_HAS_ASSET_QUANTITY_AVAILABLE_NOTIFICATION_MESSAGE = "A criação da ordem de compra não pode ser realizada, pois a quantidade de ativo financeiro é menor que a quantidade solicitada.";

            if (updateFinancialAssetQuantityAvailableAsync == 0)
                return MethodResult<INotification, CreateOrderServiceOutput>.FactoryError(
                    notifications: [Notification.FactoryFailure(
                        code: CREATE_ORDER_BUY_CANNOT_BE_DONE_BECAUSE_DOESNT_HAS_ASSET_QUANTITY_AVAILABLE_NOTIFICATION_CODE,
                        message: CREATE_ORDER_BUY_CANNOT_BE_DONE_BECAUSE_DOESNT_HAS_ASSET_QUANTITY_AVAILABLE_NOTIFICATION_MESSAGE)]);

            var orderId = IdentityValueObject.Factory();

            var order = new Order(
                id: orderId,
                createdAt: DateTime.UtcNow,
                type: input.Type,
                status: OrderStatusValueObject.EXECUTED,
                quantity: input.Quantity,
                unitaryPrice: input.FinancialAsset.UnitaryPrice,
                totalPrice: (input.Quantity * input.FinancialAsset.UnitaryPrice));
            order.CustomerId = input.CustomerId;
            order.FinancialAssetId = input.FinancialAsset.Id;

            await _orderBaseRepository.AddAsync(order, cancellationToken);

            const string CREATE_ORDER_BUY_DONE_NOTIFICATION_CODE = "CREATE_ORDER_BUY_DONE";
            const string CREATE_ORDER_BUY_DONE_NOTIFICATION_MESSAGE = "A criação da ordem de compra do ativo financeiro foi realizada com sucesso.";

            await _unitOfWork.ApplyDataContextTransactionChangeAsync(cancellationToken);

            return MethodResult<INotification, CreateOrderServiceOutput>.FactorySuccess(
                notifications: [Notification.FactorySuccess(
                    code: CREATE_ORDER_BUY_DONE_NOTIFICATION_CODE,
                    message: CREATE_ORDER_BUY_DONE_NOTIFICATION_MESSAGE)],
                output: CreateOrderServiceOutput.Factory(order));
        }
        else
        {
            await _extensionFinancialAssetRepository.UpdateFinancialAssetWithOrderSellProcessAync(
                financialAssetId: input.FinancialAsset.Id,
                quantityWillSell: input.Quantity,
                cancellationToken: cancellationToken);

            var orderId = IdentityValueObject.Factory();

            var order = new Order(
                id: orderId,
                createdAt: DateTime.UtcNow,
                type: input.Type,
                status: OrderStatusValueObject.EXECUTED,
                quantity: input.Quantity,
                unitaryPrice: input.FinancialAsset.UnitaryPrice,
                totalPrice: (input.Quantity * input.FinancialAsset.UnitaryPrice));
            order.CustomerId = input.CustomerId;
            order.FinancialAssetId = input.FinancialAsset.Id;

            await _orderBaseRepository.AddAsync(order, cancellationToken);

            const string CREATE_ORDER_SELL_DONE_NOTIFICATION_CODE = "CREATE_ORDER_SELL_DONE";
            const string CREATE_ORDER_SELL_DONE_NOTIFICATION_MESSAGE = "A criação da ordem de venda do ativo financeiro foi realizada com sucesso.";

            await _unitOfWork.ApplyDataContextTransactionChangeAsync(cancellationToken);

            return MethodResult<INotification, CreateOrderServiceOutput>.FactorySuccess(
                notifications: [Notification.FactorySuccess(
                    code: CREATE_ORDER_SELL_DONE_NOTIFICATION_CODE,
                    message: CREATE_ORDER_SELL_DONE_NOTIFICATION_MESSAGE)],
                output: CreateOrderServiceOutput.Factory(order));
        }
    }
}
