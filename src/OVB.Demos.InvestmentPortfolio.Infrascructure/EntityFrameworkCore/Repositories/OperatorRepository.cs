using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class OperatorRepository : BaseRepository<Operator>, IExtensionOperatorRepository
{
    public OperatorRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<Operator?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Operator>().Where(p => p.Id.GetIdentity() == id).FirstOrDefaultAsync(cancellationToken);
}
