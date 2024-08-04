using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.PortfolioContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext.Fakes;

public sealed class FakerPortfolioRepository : IBaseRepository<Portfolio>, IExtensionPortfolioRepository
{
    public Task AddAsync(Portfolio entity, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AddRangeAsync(Portfolio[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Portfolio?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Portfolio?> QueryPortfolioByFinancialAssetIdAndCustomerIdAsNoTrackingAsync(IdentityValueObject financialAssetId, IdentityValueObject customerId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Portfolio[]> QueryPortfoliosByCustomerIdAndPaginationIncludingFinancialAssetAsNoTrackingAsync(IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => Task.FromResult((Portfolio[])[PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS]);

    public Task RemoveAsync(Portfolio entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(Portfolio[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Portfolio entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdatePortfolioQuantityAndTotalPriceInvestedAsync(IdentityValueObject financialAssetId, IdentityValueObject customerId, QuantityValueObject additionalQuantity, TotalPriceValueObject additionalPrice, CancellationToken cancellationToken)
        => Task.FromResult(1);

    public Task UpdateRangeAsync(Portfolio[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
