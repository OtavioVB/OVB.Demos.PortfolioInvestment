using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;

public readonly struct CreatePortfolioServiceOutput
{
    public Portfolio Portfolio { get; }

    private CreatePortfolioServiceOutput(Portfolio portfolio)
    {
        Portfolio = portfolio;
    }

    public static CreatePortfolioServiceOutput Factory(Portfolio portfolio)
        => new(portfolio);
}
