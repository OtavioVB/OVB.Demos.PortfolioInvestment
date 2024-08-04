using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OrderContext.Fakes;

public sealed class FakerOrderRepository : IBaseRepository<Order>, IExtensionOrderRepository
{
    public Task AddAsync(Order entity, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AddRangeAsync(Order[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Order entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(Order[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(Order[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
