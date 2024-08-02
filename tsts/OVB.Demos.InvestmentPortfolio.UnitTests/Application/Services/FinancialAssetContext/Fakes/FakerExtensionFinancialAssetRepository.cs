using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;

public sealed class FakerExtensionFinancialAssetRepository : IBaseRepository<FinancialAsset>, IExtensionFinancialAssetRepository
{
    public readonly bool _existsSymbol;

    public FakerExtensionFinancialAssetRepository(bool existsSymbol)
    {
        _existsSymbol = existsSymbol;
    }

    public Task AddAsync(FinancialAsset entity, CancellationToken cancellationToken)
        => Task.Run(Console.WriteLine);

    public Task AddRangeAsync(FinancialAsset[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<FinancialAsset?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(FinancialAsset entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(FinancialAsset[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(FinancialAsset entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(FinancialAsset[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyFinancialAssetExistsBySymbolAsync(AssetSymbolValueObject symbol, CancellationToken cancellationToken)
        => Task.FromResult(_existsSymbol);
}
