using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class QueryFinancialByIdAssetServiceValidationTests
{
    [Fact]
    public async void QueryById_Financial_Asset_Service_Should_Be_Executed_As_Expected()
    {
        // Arrange
        var repository = new FakerExtensionFinancialAssetRepository(
            existsSymbol: false,
            existsFinancialAssetOnGet: true);

        var service = new FinancialAssetService(
            extensionFinancialAssetRepository: repository,
            baseFinancialAssetRepository: repository,
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_NOTIFICATION_CODE = "QUERY_BY_ID_FINANCIAL_ASSET_HAS_DONE";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A consulta do ativo financeiro pela sua identificação foi realizada com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await service.QueryByIdFinancialAssetServiceAsync(
            input: QueryByIdFinancialAssetServiceInput.Factory(
                financialAssetId: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE.Id),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
        Assert.Equal(serviceResult.Output.FinancialAsset, FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE);
    }

    [Fact]
    public async void QueryById_Financial_Asset_Service_Should_Result_Error_When_Not_Found_Financial_Asset()
    {
        // Arrange
        var repository = new FakerExtensionFinancialAssetRepository(
            existsSymbol: false,
            existsFinancialAssetOnGet: false);

        var service = new FinancialAssetService(
            extensionFinancialAssetRepository: repository,
            baseFinancialAssetRepository: repository,
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_NOTIFICATION_CODE = "FINANCIAL_ASSET_NOT_FOUND";
        const string EXPECTED_NOTIFICATION_MESSAGE = "O ativo financeiro enviado não foi possível ser encontrado.";
        const string EXPECTED_NOTIFICATION_TYPE = "Failure";

        // Act
        var serviceResult = await service.QueryByIdFinancialAssetServiceAsync(
            input: QueryByIdFinancialAssetServiceInput.Factory(
                financialAssetId: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE.Id),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(serviceResult.IsSuccess);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }
}
