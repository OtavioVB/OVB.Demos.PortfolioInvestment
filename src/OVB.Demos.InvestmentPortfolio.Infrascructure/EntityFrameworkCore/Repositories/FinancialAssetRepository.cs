using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
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
}
