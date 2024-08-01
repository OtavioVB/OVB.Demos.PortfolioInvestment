using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class OrderRepository : BaseRepository<Order>, IExtensionOrderRepository
{
    public OrderRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<Order?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Order>().Where(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
}
