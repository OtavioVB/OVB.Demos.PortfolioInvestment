using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Outputs;

public readonly struct CreateExtractServiceOutput
{
    public Extract Extract { get; }

    private CreateExtractServiceOutput(Extract extract)
    {
        Extract = extract;
    }

    public static CreateExtractServiceOutput Factory(Extract extract)
        => new(extract);
}
