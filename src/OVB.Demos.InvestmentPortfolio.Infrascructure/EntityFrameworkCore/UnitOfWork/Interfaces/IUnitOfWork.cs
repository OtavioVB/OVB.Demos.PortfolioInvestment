using System.Data;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    public Task ApplyDataContextTransactionChangeAsync(CancellationToken cancellationToken);

    public Task<TOutput> ExecuteUnitOfWorkAsync<TInput, TOutput>(
        TInput input,
        Func<TInput, CancellationToken, Task<(bool ExecuteUnitOfWork, TOutput Output)>> handler,
        CancellationToken cancellationToken,
        IsolationLevel isolationLevel = IsolationLevel.RepeatableRead);
}
