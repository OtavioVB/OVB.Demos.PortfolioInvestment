using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext;

public sealed class PortfolioService : IPortfolioService
{
    private readonly IBaseRepository<Portfolio> _basePortfolioRepository;
    private readonly IExtensionPortfolioRepository _extensionPortfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioService(
        IBaseRepository<Portfolio> basePortfolioRepository,
        IExtensionPortfolioRepository extensionPortfolioRepository,
        IUnitOfWork unitOfWork)
    {
        _basePortfolioRepository = basePortfolioRepository;
        _extensionPortfolioRepository = extensionPortfolioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MethodResult<INotification>> CreateOrUpdatePortfolioServiceAsync(
        CreateOrUpdatePortfolioServiceInput input, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return  inputValidationResult;

        var additionalTotalPrice = (input.Quantity * input.UnitaryPrice);

        var updatePortfolioItemsChangedResult = await _extensionPortfolioRepository.UpdatePortfolioQuantityAndTotalPriceInvestedAsync(
            financialAssetId: input.FinancialAssetId,
            customerId: input.CustomerId,
            additionalQuantity:  input.Quantity,
            additionalPrice: additionalTotalPrice,
            cancellationToken: cancellationToken);

        if (updatePortfolioItemsChangedResult == 0)
        {
            var portfolioId = IdentityValueObject.Factory();

            var portfolio = new Portfolio(
                id: portfolioId,
                totalPrice: additionalTotalPrice,
                quantity: input.Quantity);
            portfolio.CustomerId = input.CustomerId;
            portfolio.FinancialAssetId = input.FinancialAssetId;

            await _basePortfolioRepository.AddAsync(portfolio, cancellationToken);
        }

        await _unitOfWork.ApplyDataContextTransactionChangeAsync(cancellationToken);

        const string PORTFOLIO_UPDATE_HAS_DONE_NOTIFICATION_CODE = "PORTFOLIO_UPDATE_HAS_DONE";
        const string PORTFOLIO_UPDATE_HAS_DONE_NOTIFICATION_MESSAGE = "O portfólio dos ativos financeiros foram atualizados com sucesso.";

        return MethodResult<INotification>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: PORTFOLIO_UPDATE_HAS_DONE_NOTIFICATION_CODE,
                message: PORTFOLIO_UPDATE_HAS_DONE_NOTIFICATION_MESSAGE)]);
    }

    public async Task<MethodResult<INotification, QueryPortfolioServiceOutput>> QueryPortfolioServiceAsync(
        QueryPortfolioServiceInput input, CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, QueryPortfolioServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var portfolios = await _extensionPortfolioRepository.QueryPortfoliosByCustomerIdAndPaginationIncludingFinancialAssetAsNoTrackingAsync(
            customerId: input.CustomerId,
            page: input.Page,
            offset: input.Offset,
            cancellationToken: cancellationToken);

        const string QUERY_PORTFOLIOS_HAS_DONE_SUCCESSFULL_NOTIFICATION_CODE = "QUERY_PORTFOLIOS_HAS_DONE_SUCCESSFULL";
        const string QUERY_PORTFOLIOS_HAS_DONE_SUCCESSFULL_NOTIFICATION_MESSAGE = "A consulta dos portfólios foi realizada com sucesso.";

        return MethodResult<INotification, QueryPortfolioServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: QUERY_PORTFOLIOS_HAS_DONE_SUCCESSFULL_NOTIFICATION_CODE,
                message: QUERY_PORTFOLIOS_HAS_DONE_SUCCESSFULL_NOTIFICATION_MESSAGE)],
            output: QueryPortfolioServiceOutput.Factory(
                page: input.Page,
                offset: input.Offset,
                items: portfolios));
    }
}
