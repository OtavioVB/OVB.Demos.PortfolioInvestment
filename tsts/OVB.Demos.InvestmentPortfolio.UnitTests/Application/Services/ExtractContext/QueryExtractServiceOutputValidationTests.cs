using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ExtractContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext;

public sealed class QueryExtractServiceOutputValidationTEsts
{
    [Fact]
    public void Query_Extract_Service_Output_Should_Be_Valid()
    {
        // Arrange
        var page = 1;
        var offset = 25;
        Extract[] expected = [ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE];

        // Act
        var output = QueryExtractServiceOutput.Factory(
            page: page,
            offset: offset,
            items: expected);

        // Assert
        Assert.Equal(page, output.Page.GetPage());
        Assert.Equal(offset, output.Offset.GetOffset());
        Assert.Equal(expected, output.Items);
    }
}
