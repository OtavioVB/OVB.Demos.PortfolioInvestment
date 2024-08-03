using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class FinancialAssetRepository : BaseRepository<FinancialAsset>, IExtensionFinancialAssetRepository
{
    public FinancialAssetRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<FinancialAsset?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().Where(p => p.Id.GetIdentity() == id).FirstOrDefaultAsync(cancellationToken);

    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingByPaginationAsync(PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().AsNoTracking().Skip(page.GetIndex()).Take(offset).ToArrayAsync(cancellationToken);

    public Task<bool> VerifyFinancialAssetExistsBySymbolAsync(AssetSymbolValueObject symbol, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().AsNoTracking().Where(p => p.Symbol == symbol).AnyAsync(cancellationToken);
}
