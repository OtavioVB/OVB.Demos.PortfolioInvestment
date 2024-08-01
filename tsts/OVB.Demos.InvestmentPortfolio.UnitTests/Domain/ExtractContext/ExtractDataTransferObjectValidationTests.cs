using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ExtractContext;

public sealed class ExtractDataTransferObjectValidationTests
{
    public static Extract EXTRACT_EXAMPLE = new Extract(
        id: Guid.NewGuid(),
        createdAt: DateTime.Parse("2024-07-31T09:42:37Z"),
        type: ExtractType.SELL,
        totalPrice: 15,
        unitaryPrice: 5,
        quantity: 3)
    {
        Customer = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE,
        FinancialAsset = FinancialAssertDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE
    };
    
    [Theory]
    [InlineData("2024-07-31T09:42:37Z", ExtractType.BUY, 15, 5, 3)]
    public void Extract_Data_Transfer_Object_Should_Be_Equal_Expected(string extractDate, ExtractType type, decimal totalPrice, decimal unitaryPrice, decimal quantity)
    {
        // Arrange
        var extractId = Guid.NewGuid();
        var extractAt = DateTime.Parse(extractDate);

        // Act
        var extract = new Extract(
            id: extractId,
            createdAt: extractAt,
            type: type,
            totalPrice: totalPrice,
            unitaryPrice: unitaryPrice,
            quantity: quantity);
        extract.Customer = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE;
        extract.FinancialAsset = FinancialAssertDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        // Assert
        Assert.Equal(extractId, extract.Id);
        Assert.Equal(extractAt, extract.CreatedAt);
        Assert.Equal(type, extract.Type.GetExtractType());
        Assert.Equal(totalPrice, extract.TotalPrice.GetTotalPrice());
        Assert.Equal(unitaryPrice, extract.UnitaryPrice.GetUnitaryPrice());
        Assert.Equal(quantity, extract.Quantity.GetQuantityAvailable());
        Assert.Equal(CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE, extract.Customer);
        Assert.Equal(FinancialAssertDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE, extract.FinancialAsset);
    }
}
