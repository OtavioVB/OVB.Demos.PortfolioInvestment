using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.CustomerContext.Fakes;

public class FakerExtensionCustomerRepository : IBaseRepository<Customer>, IExtensionCustomerRepository
{
    private readonly bool _customerExists;

    public FakerExtensionCustomerRepository(bool customerExists)
    {
        _customerExists = customerExists;
    }

    public Task AddAsync(Customer entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(Customer[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerByEmailAsNoTrackingAsync(EmailValueObject email, CancellationToken cancellationToken)
        => Task.FromResult(_customerExists ? (Customer?)CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE : null);

    public Task<Customer?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Customer entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync(Customer[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Customer entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(Customer[] entities, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
