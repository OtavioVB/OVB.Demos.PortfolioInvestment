using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

public interface IExtensionCustomerRepository
{
    public Task<Customer?> GetCustomerByEmailAsNoTrackingAsync(EmailValueObject email, CancellationToken cancellationToken);
}
