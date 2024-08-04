using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;
using System.Data;

namespace OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder;

public sealed class CreateOrderUseCase : IUseCase<CreateOrderUseCaseInput, CreateOrderUseCaseOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderService _orderService;
    private readonly IFinancialAssetService _financialAssetService;
    private readonly IPortfolioService _portfolioService;
    private readonly IExtractService _extractService;

    public CreateOrderUseCase(
        IUnitOfWork unitOfWork, 
        IOrderService orderService, 
        IFinancialAssetService financialAssetService, 
        IPortfolioService portfolioService, 
        IExtractService extractService)
    {
        _unitOfWork = unitOfWork;
        _orderService = orderService;
        _financialAssetService = financialAssetService;
        _portfolioService = portfolioService;
        _extractService = extractService;
    }

    public Task<MethodResult<INotification, CreateOrderUseCaseOutput>> ExecuteUseCaseOrchestratorAsync(
        CreateOrderUseCaseInput input,
        CancellationToken cancellationToken)
        => _unitOfWork.ExecuteUnitOfWorkAsync(
            input: input,
            handler: async (input, cancellationToken) =>
            {
                var financialAssetServiceResult = await _financialAssetService.QueryByIdFinancialAssetServiceAsync(
                    input: QueryByIdFinancialAssetServiceInput.Factory(
                        financialAssetId: input.FinancialAssetId),
                    cancellationToken: cancellationToken);

                if (financialAssetServiceResult.IsError)
                    return (false, MethodResult<INotification, CreateOrderUseCaseOutput>.FactoryError(
                        notifications: financialAssetServiceResult.Notifications));

                var createOrderServiceResult = await _orderService.CreateOrderServiceAsync(
                    input: CreateOrderServiceInput.Factory(
                        customerId: input.CustomerId,
                        type: input.Type,
                        quantity: input.Quantity,
                        financialAsset: financialAssetServiceResult.Output.FinancialAsset),
                    cancellationToken: cancellationToken);

                if (createOrderServiceResult.IsError)
                    return (false, MethodResult<INotification, CreateOrderUseCaseOutput>.FactoryError(
                        notifications: createOrderServiceResult.Notifications));

                var createOrUpdatePortfolioServiceResult = await _portfolioService.CreateOrUpdatePortfolioServiceAsync(
                    input: CreateOrUpdatePortfolioServiceInput.Factory(
                        customerId: input.CustomerId,
                        financialAssetId: input.FinancialAssetId,
                        quantity: input.Type.GetOrderType() == OrderType.BUY ? input.Quantity : input.Quantity*-1,
                        unitaryPrice: createOrderServiceResult.Output.Order!.UnitaryPrice),
                    cancellationToken: cancellationToken);

                if(createOrUpdatePortfolioServiceResult.IsError)
                    return (false, MethodResult<INotification, CreateOrderUseCaseOutput>.FactoryError(
                        notifications: createOrUpdatePortfolioServiceResult.Notifications));

                var createExtractServiceResult = await _extractService.CreateExtractServiceAsync(
                    input: CreateExtractServiceInput.Factory(
                        createdAt: createOrderServiceResult.Output.Order.CreatedAt,
                        type: createOrderServiceResult.Output.Order.Type.GetOrderTypeAsString(),
                        totalPrice: createOrderServiceResult.Output.Order.TotalPrice,
                        unitaryPrice: createOrderServiceResult.Output.Order.UnitaryPrice,
                        quantity: createOrderServiceResult.Output.Order.Quantity,
                        customerId: input.CustomerId,
                        financialAssetId: input.FinancialAssetId),
                    cancellationToken: cancellationToken);

                if (createExtractServiceResult.IsError)
                    return (false, MethodResult<INotification, CreateOrderUseCaseOutput>.FactoryError(
                        notifications: createExtractServiceResult.Notifications));

                return (true, MethodResult<INotification, CreateOrderUseCaseOutput>.FactorySuccess(
                    notifications: createOrderServiceResult.Notifications,
                    output: CreateOrderUseCaseOutput.Factory(
                        order: createOrderServiceResult.Output.Order,
                        financialAsset: financialAssetServiceResult.Output.FinancialAsset)));
            },
            cancellationToken: cancellationToken,
            isolationLevel: IsolationLevel.Serializable);
}
