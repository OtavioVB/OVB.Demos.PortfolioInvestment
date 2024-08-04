using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext;

public sealed class CreateOrUpdatePortfolioServiceInputValidationTests
{
    [Fact]
    public void CreateOrUpdateServiceInput_Should_Be_Valid()
    {
        // Arrange
        var customerId = IdentityValueObject.Factory();
        var financialAssetId = IdentityValueObject.Factory();
        var quantity = 2;
        var unitaryPrice = 5;

        // Act
        var input = CreateOrUpdatePortfolioServiceInput.Factory(
            customerId: customerId,
            financialAssetId: financialAssetId,
            quantity: quantity,
            unitaryPrice: unitaryPrice);

        // Assert
        Assert.True(input.GetInputValidationResult().IsSuccess);
        Assert.Empty(input.GetInputValidationResult().Notifications);
        Assert.Equal(customerId, input.CustomerId);
        Assert.Equal(financialAssetId, input.FinancialAssetId);
        Assert.Equal(quantity, input.Quantity);
        Assert.Equal(unitaryPrice, input.UnitaryPrice);
    }
}
