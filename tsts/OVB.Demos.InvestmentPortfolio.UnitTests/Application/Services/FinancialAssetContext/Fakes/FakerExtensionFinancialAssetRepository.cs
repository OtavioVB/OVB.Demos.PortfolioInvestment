using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;

public sealed class FakerExtensionFinancialAssetRepository : IBaseRepository<FinancialAsset>, IExtensionFinancialAssetRepository
{
    public readonly bool _existsSymbol;
    public readonly bool _existsFinancialAssetOnGet;
    public readonly bool _financialAssetQuantityBuyIsGreaterThanTheAllowed;

    public FakerExtensionFinancialAssetRepository(bool existsSymbol, bool existsFinancialAssetOnGet = true, bool financialAssetQuantityBuyIsGreaterThanTheAllowed = true)
    {
        _existsSymbol = existsSymbol;
        _existsFinancialAssetOnGet = existsFinancialAssetOnGet;
        _financialAssetQuantityBuyIsGreaterThanTheAllowed = financialAssetQuantityBuyIsGreaterThanTheAllowed;
    }

    public Task AddAsync(FinancialAsset entity, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AddRangeAsync(FinancialAsset[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FinancialAsset?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _existsFinancialAssetOnGet 
            ? Task.FromResult((FinancialAsset?)FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE)
            : Task.FromResult((FinancialAsset?)null);

    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingByPaginationAsync(PageValueObject page, OffsetValueObject offset, CancellationToken cancellationToken)
        => Task.FromResult(_existsFinancialAssetOnGet == true ? (FinancialAsset[])[FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE] : []);

    public Task<FinancialAsset[]> QueryFinancialAssetAsNoTrackingWhenExpirationDateIsLessThanExpectedDateIncludingOperatorsAsync(DateTime expirationDateExpected, CancellationToken cancellationToken)
        => Task.FromResult(_existsFinancialAssetOnGet == true ? (FinancialAsset[])[FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE] : []);

    public Task RemoveAsync(FinancialAsset entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(FinancialAsset[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(FinancialAsset entity, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task<int> UpdateFinancialAssetIfBuyProcessQuantityIsGreaterThanTheMinimumAsync(IdentityValueObject financialAssetId, decimal quantityWillBuy, CancellationToken cancellationToken)
        => _financialAssetQuantityBuyIsGreaterThanTheAllowed == true ? Task.FromResult(1) : Task.FromResult(0);


    public Task UpdateFinancialAssetWithOrderSellProcessAync(IdentityValueObject financialAssetId, decimal quantityWillSell, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task UpdateRangeAsync(FinancialAsset[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyFinancialAssetExistsBySymbolAsync(AssetSymbolValueObject symbol, CancellationToken cancellationToken)
        => Task.FromResult(_existsSymbol);
}
