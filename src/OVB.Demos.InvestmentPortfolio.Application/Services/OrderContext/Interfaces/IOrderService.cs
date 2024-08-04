using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Interfaces;

public interface IOrderService
{
    public Task<MethodResult<INotification, CreateOrderServiceOutput>> CreateOrderServiceAsync(
        CreateOrderServiceInput input,
        CancellationToken cancellationToken);
}
