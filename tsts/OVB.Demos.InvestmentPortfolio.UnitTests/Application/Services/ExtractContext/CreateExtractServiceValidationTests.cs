using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ExtractContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.ExtractContext;

public sealed class CreateExtractServiceValidationTests
{
    [Fact]
    public async void Create_Extract_Service_Should_Result_Equal_Expected()
    {
        // Arrange
        var repository = new FakerExtractRepository();

        var service = new ExtractService(
            extractBaseRepository: repository,
            unitOfWork: new FakerUnitOfWork(),
            extensionExtractRepository: repository);

        const string EXPECTED_NOTIFICATION_CODE = "CREATE_EXTRACT_HAS_DONE";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A criação do item extratual foi realizada com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await service.CreateExtractServiceAsync(
            input: CreateExtractServiceInput.Factory(
                createdAt: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.CreatedAt,
                type: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.Type,
                totalPrice: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.TotalPrice,
                unitaryPrice: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.UnitaryPrice,
                quantity: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.Quantity,
                customerId: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.CustomerId,
                financialAssetId: ExtractDataTransferObjectValidationTests.EXTRACT_EXAMPLE.FinancialAssetId),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }
}
