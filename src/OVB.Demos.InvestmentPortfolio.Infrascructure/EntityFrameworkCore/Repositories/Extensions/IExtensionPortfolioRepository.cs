using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

public interface IExtensionPortfolioRepository
{
    public Task<int> UpdatePortfolioQuantityAndTotalPriceInvestedAsync(
        IdentityValueObject financialAssetId, IdentityValueObject customerId,
        QuantityValueObject additionalQuantity, TotalPriceValueObject additionalPrice,
        CancellationToken cancellationToken);
    public Task<Portfolio?> QueryPortfolioByFinancialAssetIdAndCustomerIdAsNoTrackingAsync(
        IdentityValueObject financialAssetId, 
        IdentityValueObject customerId,
        CancellationToken cancellationToken);
}
