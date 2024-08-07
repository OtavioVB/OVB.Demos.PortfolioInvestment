﻿using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Interfaces;

public interface IPortfolioService
{
    public Task<MethodResult<INotification>> CreateOrUpdatePortfolioServiceAsync(
        CreateOrUpdatePortfolioServiceInput input,
        CancellationToken cancellationToken);

    public Task<MethodResult<INotification, QueryPortfolioServiceOutput>> QueryPortfolioServiceAsync(
        QueryPortfolioServiceInput input,
        CancellationToken cancellationToken);
}
