using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Outputs;

public readonly struct QueryPortfolioServiceOutput
{
    public PageValueObject Page { get; }
    public OffsetValueObject Offset { get; }
    public Portfolio[] Items { get; }

    private QueryPortfolioServiceOutput(PageValueObject page, OffsetValueObject offset, Portfolio[] items)
    {
        Page = page;
        Offset = offset;
        Items = items;
    }

    public static QueryPortfolioServiceOutput Factory(PageValueObject page, OffsetValueObject offset, Portfolio[] items)
        => new(page, offset, items);
}
