using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;

public interface IFinancialAssetService
{
    public Task<MethodResult<INotification, CreateFinancialAssetServiceOutput>> CreateFinancialAssetServiceAsync(
        CreateFinancialAssetServiceInput input,
        CancellationToken cancellationToken);

    public Task<MethodResult<INotification, UpdateFinancialAssetServiceOutput>> UpdateFinancialAssetServiceAsync(
        UpdateFinancialAssetServiceInput input,
        CancellationToken cancellationToken);

    public Task<MethodResult<INotification, QueryFinancialAssetServiceOutput>> QueryFinancialAssetServiceAsync(
        QueryFinancialAssetServiceInput input,
        CancellationToken cancellationToken);

    public Task<MethodResult<INotification>> AdviceFinancialAssetUpcomingExpirationDateAsync(
        CancellationToken cancellationToken);

    public Task<MethodResult<INotification, QueryByIdFinancialAssetServiceOutput>> QueryByIdFinancialAssetServiceAsync(
        QueryByIdFinancialAssetServiceInput input,
        CancellationToken cancellationToken);
}
