using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.UseCases.Interfaces;

public interface IUseCase<TInput, TOutput>
{
    public Task<MethodResult<INotification, TOutput>> ExecuteUseCaseOrchestratorAsync(
        TInput input,
        CancellationToken cancellationToken);
}
