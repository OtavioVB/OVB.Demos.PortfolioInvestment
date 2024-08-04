using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.PortfolioContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext;

public sealed class CreateOrUpdatePortfolioServiceValidationTests
{
    [Fact]
    public async void Create_Or_Update_Portfolio_Service_Should_Result_Success_As_Expected()
    {
        // Arrange
        var repository = new FakerPortfolioRepository();

        var service = new PortfolioService(
            basePortfolioRepository: repository,
            extensionPortfolioRepository: repository,
            unitOfWork: new FakerUnitOfWork());

        const string EXPECTED_NOTIFICATION_CODE = "PORTFOLIO_UPDATE_HAS_DONE";
        const string EXPECTED_NOTIFICATION_MESSAGE = "O portfólio dos ativos financeiros foram atualizados com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await service.CreateOrUpdatePortfolioServiceAsync(
            input: CreateOrUpdatePortfolioServiceInput.Factory(
                customerId: PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS.CustomerId,
                financialAssetId: PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS.FinancialAssetId,
                quantity: PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS.Quantity,
                unitaryPrice: PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS.TotalPrice.GetTotalPrice()),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Create_Or_Update_Portfolio_Service_Should_Result_Error_If_Any_Params_Is_Not_Valid()
    {
        // Arrange
        var repository = new FakerPortfolioRepository();

        var service = new PortfolioService(
            basePortfolioRepository: repository,
            extensionPortfolioRepository: repository,
            unitOfWork: new FakerUnitOfWork());

        var customerId = PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS.CustomerId;
        var financialAssetId = PortfolioDataTransferObjectValidationTests.PORTFLIO_EXAMPLE_TESTS.FinancialAssetId;
        QuantityValueObject quantity = 0;
        UnitaryPriceValueObject unitaryPrice = -1;

        var expectedMethodResult = MethodResult<INotification>.Factory(customerId, financialAssetId, quantity, unitaryPrice);

        // Act
        var serviceResult = await service.CreateOrUpdatePortfolioServiceAsync(
            input: CreateOrUpdatePortfolioServiceInput.Factory(
                customerId: customerId,
                financialAssetId: financialAssetId,
                quantity: quantity,
                unitaryPrice: unitaryPrice),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(serviceResult.IsSuccess);
        Assert.NotEmpty(serviceResult.Notifications);
        Assert.Equal(expectedMethodResult.Notifications, serviceResult.Notifications);
    }
}
