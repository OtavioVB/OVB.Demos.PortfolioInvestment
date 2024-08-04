using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ExtractContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext.Fakes;

public sealed class FakerExtractRepository : IBaseRepository<Extract>, IExtensionExtractRepository
{


    public Task AddAsync(Extract entity, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AddRangeAsync(Extract[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Extract?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Extract[]> QueryExtractByCustomerIdAsNoTrackingIncludingFinanceAssetAsync(IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => Task.FromResult((Extract[])[ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE]);

    public Task RemoveAsync(Extract entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(Extract[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Extract entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(Extract[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
