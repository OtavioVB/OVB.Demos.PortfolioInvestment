using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

public interface IExtensionOperatorRepository
{
    public Task<Operator?> GetOperatorByEmailAsNoTrackingAsync(EmailValueObject email, CancellationToken cancellationToken);
}
