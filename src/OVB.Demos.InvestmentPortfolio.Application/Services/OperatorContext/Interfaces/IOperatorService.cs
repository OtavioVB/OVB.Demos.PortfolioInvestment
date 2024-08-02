using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Interfaces;

public interface IOperatorService
{
    public Task<MethodResult<INotification, OAuthOperatorAuthenticationServiceOutput>> OAuthOperatorAuthenticationServiceAsync(
        OAuthOperatorAuthenticationServiceInput input,
        CancellationToken cancellationToken);
}
