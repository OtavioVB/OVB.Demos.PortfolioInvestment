using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

public interface IExtensionFinancialAssetRepository
{
    public Task<bool> VerifyFinancialAssetExistsBySymbolAsync(AssetSymbolValueObject symbol, CancellationToken cancellationToken);
    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingByPaginationAsync(PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken);
    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingWhenExpirationDateIsLessThanExpectedDateIncludingOperatorsAsync(DateTime expirationDateExpected, CancellationToken cancellationToken);
    public Task UpdateFinancialAssetWithOrderSellProcessAync(IdentityValueObject financialAssetId, QuantityValueObject quantityWillSell, CancellationToken cancellationToken);
    public Task<int> UpdateFinancialAssetIfBuyProcessQuantityIsGreaterThanTheMinimumAsync(IdentityValueObject financialAssetId, QuantityValueObject quantityWillBuy, CancellationToken cancellationToken);
}
