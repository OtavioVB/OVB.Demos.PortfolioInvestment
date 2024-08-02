using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OperatorContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OperatorContext.Fakes;

public sealed class FakerExtensionOperatorRepository : IExtensionOperatorRepository
{
    public static FakerExtensionOperatorRepository Repository => new FakerExtensionOperatorRepository();

    public Task<Operator?> GetOperatorByEmailAsNoTrackingAsync(EmailValueObject email, CancellationToken cancellationToken)
    {
        if (email == OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE.Email)
            return Task.FromResult((Operator?)OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE);

        return Task.FromResult((Operator?)null);
    }
}
