using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Outputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ExtractContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext;

public sealed class CreateExtractServiceOutputValidationTests
{
    [Fact]
    public void CreateExtractServiceOutput_Should_Be_Valid()
    {
        // Arrange

        // Act
        var output = CreateExtractServiceOutput.Factory(
            extract: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE);

        // Assert
        Assert.Equal(ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE, output.Extract);
    }
}
