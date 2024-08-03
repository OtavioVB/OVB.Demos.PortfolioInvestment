using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class QueryFinancialAssetServiceValidationTests
{
    [Fact]
    public async void Query_Financial_Asset_Service_Should_Be_Queried()
    {
        // Arrange
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: true),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: true),
            unitOfWork: new FakerUnitOfWork());

        const int PAGE = 5;
        const int OFFSET = 25;

        var input = QueryFinancialAssetServiceInput.Factory(
            page: PAGE,
            offset: OFFSET);

        const string EXPECTED_SUCCESS_NOTIFICATION_CODE = "QUERY_FINANCIAL_ASSETS_SUCCESS";
        const string EXPECTED_SUCCESS_NOTIFICATION_MESSAGE = "A consulta dos ativos financeiros foi realizada com sucesso.";
        const string EXPECTED_SUCCESS_NOTIFICATION_TYPE = "Success";

        // Act
        var queryFinancialAssetServiceResult = await financialAssetService.QueryFinancialAssetServiceAsync(
            input: QueryFinancialAssetServiceInput.Factory(
                page: PAGE,
                offset: OFFSET),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(queryFinancialAssetServiceResult.IsSuccess);
        Assert.Single(queryFinancialAssetServiceResult.Notifications);
        Assert.Equal<int>(PAGE, queryFinancialAssetServiceResult.Output.Page);
        Assert.Equal<int>(OFFSET, queryFinancialAssetServiceResult.Output.Offset);
        Assert.Equal([FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE], queryFinancialAssetServiceResult.Output.FinancialAssets);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_CODE, queryFinancialAssetServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_MESSAGE, queryFinancialAssetServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_TYPE, queryFinancialAssetServiceResult.Notifications[0].Type);
    }
}