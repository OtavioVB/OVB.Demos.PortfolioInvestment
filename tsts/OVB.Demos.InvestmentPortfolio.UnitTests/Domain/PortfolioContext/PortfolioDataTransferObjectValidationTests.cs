using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.PortfolioContext;

public sealed class PortfolioDataTransferObjectValidationTests
{
    [Theory]
    [InlineData(15, 3)]
    public void Portfolio_Data_Transfer_Object_Should_Be_Equal_Expected(decimal totalPrice, decimal quantity)
    {
        // Arrange
        var portfolioId = Guid.NewGuid();

        // Act
        var portfolio = new Portfolio(
            id: portfolioId,
            totalPrice: totalPrice,
            quantity: quantity)
        {
            Customer = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE,
            FinancialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE
        };

        // Assert
        Assert.Equal<Guid>(portfolioId, portfolio.Id);
        Assert.Equal(totalPrice, portfolio.TotalPrice.GetTotalPrice());
        Assert.Equal(quantity, portfolio.Quantity.GetQuantity());
        Assert.Equal(CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE, portfolio.Customer);
        Assert.Equal(FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE, portfolio.FinancialAsset);
    }
}
