using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext;

public sealed class QueryExtractServiceValidationTests
{
    [Fact]
    public async void Query_Extract_Service_Should_Result_Success_When_Process_Is_Expected()
    {
        // Arrange
        var repository = new FakerExtractRepository();

        var service = new ExtractService(
            extractBaseRepository: repository,
            unitOfWork: new FakerUnitOfWork(),
            extensionExtractRepository: repository);

        var input = QueryExtractServiceInput.Factory(
            customerId: CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id,
            page: 1,
            offset: 25);

        const string EXPECTED_NOTIFICATION_CODE = "QUERY_CUSTOMER_EXTRACTS_HAS_DONE_SUCCESS";
        const string EXPECTED_NOTIFICATION_MESSAGE = "O extrato dos ativos financeiros foi consultado com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await service.QueryExtractServiceAsync(
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
    public async void Query_Extract_Service_Should_Result_Error_When_Any_Param_Is_Invalid()
    {
        // Arrange
        var repository = new FakerExtractRepository();

        var service = new ExtractService(
            extractBaseRepository: repository,
            unitOfWork: new FakerUnitOfWork(),
            extensionExtractRepository: repository);

        var customerId = CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Id;
        var page = PageValueObject.Factory(0);
        var offset = OffsetValueObject.Factory(51);

        var input = QueryExtractServiceInput.Factory(
            customerId: customerId,
            page: page,
            offset: offset);

        var expectedMethodResult = MethodResult<INotification>.Factory(customerId, page, offset);

        // Act
        var serviceResult = await service.QueryExtractServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(serviceResult.IsSuccess);
        Assert.NotEmpty(serviceResult.Notifications);
        Assert.Equal(expectedMethodResult.Notifications, serviceResult.Notifications);
    }
}
