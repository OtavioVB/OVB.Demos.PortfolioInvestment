using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class ExtractRepository : BaseRepository<Extract>, IExtensionExtractRepository
{
    public ExtractRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<Extract?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Extract>().Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);

    public Task<Extract[]> QueryExtractByCustomerIdAsNoTrackingIncludingFinanceAssetAsync(IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => _dataContext.Set<Extract>().AsNoTracking().Include(p => p.FinancialAsset).Where(p => p.CustomerId == customerId).Skip(page.GetIndex() * offset.GetOffset()).Take(offset.GetOffset()).ToArrayAsync(cancellationToken);
}
