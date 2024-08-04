using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;

public readonly struct QueryExtractServiceInput
{
    public IdentityValueObject CustomerId { get; }
    public PageValueObject Page { get; }
    public OffsetValueObject Offset { get; }

    private QueryExtractServiceInput(IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset)
    {
        CustomerId = customerId;
        Page = page;
        Offset = offset;
    }

    public static QueryExtractServiceInput Factory(IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset)
        => new(customerId, page, offset);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(CustomerId, Page, Offset);
}
