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

    public Task UpdateFinancialAssetWithOrderSellProcessAync(
        IdentityValueObject financialAssetId, decimal quantityWillSell, CancellationToken cancellationToken)
        => _dataContext.Database.ExecuteSqlAsync(
            sql: $"UPDATE financial_asset.financial_assets SET quantity_available = quantity_available + {quantityWillSell} WHERE idfinancial_asset = {financialAssetId.GetIdentityAsString()} AND status = 'ACTIVE';",
            cancellationToken: cancellationToken);

    public Task<int> UpdateFinancialAssetIfBuyProcessQuantityIsGreaterThanTheMinimumAsync(
        IdentityValueObject financialAssetId, decimal quantityWillBuy, CancellationToken cancellationToken)
        => _dataContext.Database.ExecuteSqlAsync(
            sql: $"UPDATE financial_asset.financial_assets SET quantity_available = quantity_available - {quantityWillBuy} WHERE idfinancial_asset = {financialAssetId.GetIdentityAsString()} AND status = 'ACTIVE' AND (quantity_available - {quantityWillBuy}) >= 0;",
            cancellationToken: cancellationToken);

    public Task<bool> VerifyFinancialAssetExistsBySymbolAsync(AssetSymbolValueObject symbol, CancellationToken cancellationToken)
        => _dataContext.Set<FinancialAsset>().AsNoTracking().Where(p => p.Symbol == symbol).AnyAsync(cancellationToken);
}
