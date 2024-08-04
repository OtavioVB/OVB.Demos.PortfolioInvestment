using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;

public readonly struct CreateOrUpdatePortfolioServiceOutput
{
    public Portfolio Portfolio { get; }

    private CreateOrUpdatePortfolioServiceOutput(Portfolio portfolio)
    {
        Portfolio = portfolio;
    }

    public static CreateOrUpdatePortfolioServiceOutput Factory(Portfolio portfolio)
        => new(portfolio);
}
