using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
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
}
