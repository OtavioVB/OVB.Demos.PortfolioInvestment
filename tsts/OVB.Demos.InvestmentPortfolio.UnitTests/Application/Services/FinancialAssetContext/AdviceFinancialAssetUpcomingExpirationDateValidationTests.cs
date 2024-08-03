using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class AdviceFinancialAssetUpcomingExpirationDateValidationTests
{
    [Fact]
    public async void Advice_Financial_Asset_Upcoming_Expiration_Date_Should_Be_Done_With_Success()
    {
        // Arrange
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: true),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: true),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_NOTIFICATION_CODE = "FINANCIAL_ASSET_ADVICE_HAS_SENT_SUCCESS";
        const string EXPECTED_NOTIFICATION_MESSAGE = "Todos os avisos de vencimento próximo de ativos financeiros foram enviados para os gestores.";
        const string EXPECTED_NOTIFICATION_TYPE = "Failure";

        // Act
        var serviceResult = await financialAssetService.AdviceFinancialAssetUpcomingExpirationDateAsync(
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.NotEmpty(serviceResult.Notifications);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Advice_Financial_Asset_Upcoming_Expiration_Date_Should_Be_Valid_But_Nothing_Will_Run()
    {
        // Arrange
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: false),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: false),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_NOTIFICATION_CODE = "FINANCIAL_ASSET_ADVICE_NOT_FOUND";
        const string EXPECTED_NOTIFICATION_MESSAGE = "Nenhum aviso de vencimento próximo de algum ativo financeiro foi encontrado para ser enviado para os operadores gestores.";
        const string EXPECTED_NOTIFICATION_TYPE = "Information";

        // Act
        var serviceResult = await financialAssetService.AdviceFinancialAssetUpcomingExpirationDateAsync(
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.NotEmpty(serviceResult.Notifications);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }
}
