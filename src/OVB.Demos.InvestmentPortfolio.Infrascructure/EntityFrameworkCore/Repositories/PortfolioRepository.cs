using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class PortfolioRepository : BaseRepository<Portfolio>, IExtensionPortfolioRepository
{
    public PortfolioRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<Portfolio?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Portfolio>().Where(p => p.Id.GetIdentity() == id).FirstOrDefaultAsync(cancellationToken);

    public Task<Portfolio?> QueryPortfolioByFinancialAssetIdAndCustomerIdAsNoTrackingAsync(
        IdentityValueObject financialAssetId, IdentityValueObject customerId, CancellationToken cancellationToken)
        => _dataContext.Set<Portfolio>().Where(p => p.FinancialAssetId == financialAssetId && p.CustomerId == customerId).FirstOrDefaultAsync(cancellationToken);

    public Task<Portfolio[]> QueryPortfoliosByCustomerIdAndPaginationIncludingFinancialAssetAsNoTrackingAsync(IdentityValueObject customerId, PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => _dataContext.Set<Portfolio>().AsNoTracking().Include(p => p.FinancialAsset).Where(p => p.CustomerId == customerId).Skip(page.GetIndex() * offset.GetOffset()).Take(offset.GetOffset()).ToArrayAsync(cancellationToken);

    public Task<int> UpdatePortfolioQuantityAndTotalPriceInvestedAsync(
        IdentityValueObject financialAssetId, IdentityValueObject customerId,
        QuantityValueObject additionalQuantity, TotalPriceValueObject additionalPrice,
        CancellationToken cancellationToken)
        => _dataContext.Database.ExecuteSqlAsync(
            sql: $"UPDATE portfolio.portfolios SET total_price = total_price + {additionalPrice.GetTotalPrice()}, quantity = quantity + {additionalQuantity.GetQuantity()} WHERE financial_asset_id = {financialAssetId.GetIdentityAsString()} AND customer_id = {customerId.GetIdentityAsString()}",
            cancellationToken: cancellationToken);
}
