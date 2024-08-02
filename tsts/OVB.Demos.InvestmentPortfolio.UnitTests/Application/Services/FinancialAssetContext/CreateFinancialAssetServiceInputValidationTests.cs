using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class CreateFinancialAssetServiceInputValidationTests
{
    [Fact]
    public void CreateFinancialAssetServiceInput_Should_Be_Valid()
    {
        // Arrange
        var operatorId = IdentityValueObject.Factory();
        var symbol = AssetSymbolValueObject.Factory("NTN-F");
        var description = DescriptionValueObject.Factory(null);
        var expirationDate = AssetExpirationDateValueObject.Factory(DateTime.UtcNow.AddDays(7));
        var index = AssetIndexValueObject.Factory(FinancialAssetIndex.CDI.ToString());
        var type = AssetTypeValueObject.Factory("POS_FIXED");
        var status = AssetStatusValueObject.Factory("ACTIVE");
        var interestRate = InterestRateValueObject.Factory((decimal)0.9475);
        var unitaryPrice = UnitaryPriceValueObject.Factory((decimal)10.00);
        var quantityAvailable = QuantityAvailableValueObject.Factory((decimal)1297.54);

        // Act
        var input = CreateFinancialAssetServiceInput.Factory(
            operatorId: operatorId,
            symbol: symbol,
            description: description,
            expirationDate: expirationDate,
            index: index,
            type: type,
            status: status,
            interestRate: interestRate,
            unitaryPrice: unitaryPrice,
            quantityAvailable: quantityAvailable);

        // Assert
        Assert.Equal(operatorId, input.OperatorId);
        Assert.Equal(symbol, input.Symbol);
        Assert.Equal(description, input.Description);
        Assert.Equal(expirationDate, input.ExpirationDate);
        Assert.Equal(index, input.Index);
        Assert.Equal(type, input.Type);
        Assert.Equal(status, input.Status);
        Assert.Equal(interestRate, input.InterestRate);
        Assert.Equal(unitaryPrice, input.UnitaryPrice);
        Assert.Equal(quantityAvailable, input.QuantityAvailable);
        Assert.True(input.GetInputValidationResult().IsSuccess);
        Assert.Empty(input.GetInputValidationResult().Notifications);
    }
}
