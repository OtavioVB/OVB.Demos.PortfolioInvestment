using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

public interface IExtensionExtractRepository
{
    public Task<Extract[]> QueryExtractByCustomerIdAsNoTrackingIncludingFinanceAssetAsync(
        IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken);
}
