using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;

public sealed class PortfolioRepository : BaseRepository<Portfolio>, IExtensionPortfolioRepository
{
    public PortfolioRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override Task<Portfolio?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        => _dataContext.Set<Portfolio>().Where(p => p.Id.GetIdentity() == id).FirstOrDefaultAsync(cancellationToken);

    public Task<int> UpdatePortfolioQuantityAndTotalPriceInvestedAsync(
        IdentityValueObject financialAssetId, IdentityValueObject customerId, 
        QuantityValueObject additionalQuantity, TotalPriceValueObject additionalPrice, 
        CancellationToken cancellationToken)
        => _dataContext.Set<Portfolio>().Where(p => p.FinancialAssetId == financialAssetId && p.CustomerId == customerId).ExecuteUpdateAsync(p => p
            .SetProperty(q => q.TotalPrice, q => TotalPriceValueObject.Factory(q.TotalPrice.GetTotalPrice() + additionalPrice.GetTotalPrice()))
            .SetProperty(q => q.Quantity, q => QuantityValueObject.Factory(q.Quantity.GetQuantity() + additionalQuantity)),
            cancellationToken: cancellationToken);
}
