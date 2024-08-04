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
        CreatePortfolioServiceInput input, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return  inputValidationResult;

        var additionalTotalPrice = (input.Quantity * input.UnitaryPrice);

        var updatePortfolioItemsChangedResult = await _extensionPortfolioRepository.UpdatePortfolioQuantityAndTotalPriceInvestedAsync(
            financialAssetId: input.FinancialAssetId,
            customerId: input.CustomerId,
            additionalQuantity: input.Quantity,
            additionalPrice: additionalTotalPrice,
            cancellationToken: cancellationToken);

        if (updatePortfolioItemsChangedResult == 0)
        {
            var portfolioId = IdentityValueObject.Factory();

            var portfolio = new Portfolio(
                id: portfolioId,
                totalPrice: additionalTotalPrice,
                quantity: input.Quantity);

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
}
