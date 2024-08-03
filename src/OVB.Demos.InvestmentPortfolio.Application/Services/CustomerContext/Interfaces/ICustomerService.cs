using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Interfaces;

public interface ICustomerService
{
    public Task<MethodResult<INotification, OAuthCustomerAuthenticationServiceOutput>> OAuthCustomerAuthenticationServiceAsync(
        OAuthCustomerAuthenticationServiceInput input,
        CancellationToken cancellationToken);
}
