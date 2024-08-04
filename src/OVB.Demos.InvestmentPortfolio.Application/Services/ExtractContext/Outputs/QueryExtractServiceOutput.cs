using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Outputs;

public readonly struct QueryExtractServiceOutput
{
    public PageValueObject Page { get; }
    public OffsetValueObject Offset { get; }
    public Extract[] Items { get; }

    private QueryExtractServiceOutput(PageValueObject page, OffsetValueObject offset, Extract[] items)
    {
        Page = page;
        Offset = offset;
        Items = items;
    }

    public static QueryExtractServiceOutput Factory(PageValueObject page, OffsetValueObject offset, Extract[] items)
        => new(page, offset, items);
}
