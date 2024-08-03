using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;
using System.Data;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services;

public sealed class FakerUnitOfWork : IUnitOfWork
{
    public Task ApplyDataContextTransactionChangeAsync(CancellationToken cancellationToken)
        => Task.Run(Console.WriteLine);

    public Task<TOutput> ExecuteUnitOfWorkAsync<TInput, TOutput>(TInput input, Func<TInput, CancellationToken, Task<(bool ExecuteUnitOfWork, TOutput Output)>> handler, CancellationToken cancellationToken, IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
        => Task.FromResult(default(TOutput));
}
