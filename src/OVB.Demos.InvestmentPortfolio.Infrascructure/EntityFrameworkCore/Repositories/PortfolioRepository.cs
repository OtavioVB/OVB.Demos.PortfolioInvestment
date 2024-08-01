using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class PortfolioRepository : BaseRepository<Portfolio>, IExtensionPortfolioRepository
{
    public PortfolioRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<Portfolio?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Portfolio>().Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
}
