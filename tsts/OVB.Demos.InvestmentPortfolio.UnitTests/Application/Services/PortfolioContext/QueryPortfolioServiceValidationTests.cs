using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.PortfolioContext;

public sealed class QueryPortfolioServiceValidationTests
{
    [Fact]
    public async void Query_Portfolio_Service_Should_Result_With_Error_When_Any_Param_Is_Not_Valid()
    {
        // Arrange
        var repository = new FakerPortfolioRepository();

        var service = new PortfolioService(
            basePortfolioRepository: repository,
            extensionPortfolioRepository: repository,
            unitOfWork: new FakerUnitOfWork());

        var customerId = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id;
        var page = PageValueObject.Factory(1);
        var offset = OffsetValueObject.Factory(30);

        var input = QueryPortfolioServiceInput.Factory(
            customerId: customerId,
            page: page,
            offset: offset);

        const string EXPECTED_NOTIFICATION_CODE = "QUERY_PORTFOLIOS_HAS_DONE_SUCCESSFULL";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A consulta dos portfólios foi realizada com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await service.QueryPortfolioServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.Single(serviceResult.Notifications);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Query_Portfolio_Service_Should_Result_With_Success_When_Process_Execute_As_Expected()
    {
        // Arrange
        var repository = new FakerPortfolioRepository();

        var service = new PortfolioService(
            basePortfolioRepository: repository,
            extensionPortfolioRepository: repository,
            unitOfWork: new FakerUnitOfWork());

        var customerId = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id;
        var page = PageValueObject.Factory(-1);
        var offset = OffsetValueObject.Factory(-2);

        var input = QueryPortfolioServiceInput.Factory(
            customerId: customerId,
            page: page,
            offset: offset);

        var expectedMethodResult = MethodResult<INotification>.Factory(customerId, page, offset);

        // Act
        var serviceResult = await service.QueryPortfolioServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(serviceResult.IsSuccess);
        Assert.NotEmpty(serviceResult.Notifications);
        Assert.Equal(expectedMethodResult.Notifications, serviceResult.Notifications);
    }
}
