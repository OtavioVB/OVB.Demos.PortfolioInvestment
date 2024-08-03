using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class CustomerRepository : BaseRepository<Customer>, IExtensionCustomerRepository
{
    public CustomerRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<Customer?> GetCustomerByEmailAsNoTrackingAsync(EmailValueObject email, CancellationToken cancellationToken)
        => _dataContext.Set<Customer>().AsNoTracking().Where(p => p.Email == email).FirstOrDefaultAsync(cancellationToken);

    public override Task<Customer?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Customer>().Where(p => p.Id.GetIdentity() == id).FirstOrDefaultAsync(cancellationToken);
}
