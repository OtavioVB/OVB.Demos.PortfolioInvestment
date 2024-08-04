using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Interfaces;

public interface IExtractService
{
    public Task<MethodResult<INotification, CreateExtractServiceOutput>> CreateExtractServiceAsync(
        CreateExtractServiceInput input,
        CancellationToken cancellationToken);

    public Task<MethodResult<INotification, QueryExtractServiceOutput>> QueryExtractServiceAsync(
        QueryExtractServiceInput input,
        CancellationToken cancellationToken);
}
