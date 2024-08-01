namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    public Task AddRangeAsync(TEntity[] entities, CancellationToken cancellationToken);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    public Task UpdateRangeAsync(TEntity[] entities, CancellationToken cancellationToken);
    public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
    public Task RemoveRangeAsync(TEntity[] entities, CancellationToken cancellationToken);
    public Task<TEntity?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken);
}
