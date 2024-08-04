using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
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
        => _dataContext.Set<FinancialAsset>().Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);

    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingByPaginationAsync(PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().AsNoTracking().Skip(page.GetIndex()).Take(offset).ToArrayAsync(cancellationToken);

    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingWhenExpirationDateIsLessThanExpectedDateIncludingOperatorsAsync(DateTime expirationDateExpected, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>()
        .Include(p => p.Operator).AsNoTracking()
        .Where(p => p.ExpirationDate < expirationDateExpected)
        .ToArrayAsync(cancellationToken);

    public Task UpdateFinancialAssetWithOrderSellProcessAync(IdentityValueObject financialAssetId, QuantityValueObject quantityWillSell, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().Where(p => p.Id == financialAssetId && p.Status.GetStatus() == FinancialAssetStatus.ACTIVE).ExecuteUpdateAsync(p =>
        p.SetProperty(q => q.QuantityAvailable, q => QuantityAvailableValueObject.Factory(q.QuantityAvailable + quantityWillSell)));

    public Task<int> UpdateFinancialAssetIfBuyProcessQuantityIsGreaterThanTheMinimumAsync(IdentityValueObject financialAssetId, QuantityValueObject quantityWillBuy, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().Where(p => p.Id == financialAssetId && (p.QuantityAvailable - quantityWillBuy) > 0 && p.Status.GetStatus() == FinancialAssetStatus.ACTIVE).ExecuteUpdateAsync(p => 
        p.SetProperty(q => q.QuantityAvailable, q => QuantityAvailableValueObject.Factory(q.QuantityAvailable - quantityWillBuy)));

    public Task<bool> VerifyFinancialAssetExistsBySymbolAsync(AssetSymbolValueObject symbol, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().AsNoTracking().Where(p => p.Symbol == symbol).AnyAsync(cancellationToken);
}
