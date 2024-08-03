using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class CreateFinancialAssetServiceValidationTests
{
    [Fact]
    public async void Create_Financial_Asset_Service_Should_Be_Created_Financial_Asset()
    {
        // Arrange
        var financialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        var input = CreateFinancialAssetServiceInput.Factory(
            operatorId: IdentityValueObject.Factory(),
            symbol: financialAsset.Symbol,
            description: financialAsset.Description!.Value,
            expirationDate: financialAsset.ExpirationDate,
            index: financialAsset.Index,
            type: financialAsset.Type,
            status: financialAsset.Status,
            interestRate: financialAsset.InterestRate,
            unitaryPrice: financialAsset.UnitaryPrice,
            quantityAvailable: financialAsset.QuantityAvailable);
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_SUCCESS_NOTIFICATION_CODE = "FINANCIAL_ASSET_CREATED_SUCCESS";
        const string EXPECTED_SUCCESS_NOTIFICATION_MESSAGE = "O ativo financeiro enviado foi cadastrado com sucesso.";
        const string EXPECTED_SUCCESS_NOTIFICATION_TYPE = "Success";

        // Act
        var createFinancialAssetServiceResult = await financialAssetService.CreateFinancialAssetServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(createFinancialAssetServiceResult.IsSuccess);
        Assert.NotEmpty(createFinancialAssetServiceResult.Notifications);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_CODE, createFinancialAssetServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_MESSAGE, createFinancialAssetServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_TYPE, createFinancialAssetServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Create_Financial_Asset_Service_Should_Be_Error_When_Symbol_Already_Exists()
    {
        // Arrange
        var financialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        var input = CreateFinancialAssetServiceInput.Factory(
            operatorId: IdentityValueObject.Factory(),
            symbol: financialAsset.Symbol,
            description: financialAsset.Description!.Value,
            expirationDate: financialAsset.ExpirationDate,
            index: financialAsset.Index,
            type: financialAsset.Type,
            status: financialAsset.Status,
            interestRate: financialAsset.InterestRate,
            unitaryPrice: financialAsset.UnitaryPrice,
            quantityAvailable: financialAsset.QuantityAvailable);
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: true),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_ERROR_NOTIFICATION_CODE = "FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL";
        const string EXPECTED_ERROR_NOTIFICATION_MESSAGE = "O ativo financeiro enviado já existe para o símbolo associado enviado.";
        const string EXPECTED_ERROR_NOTIFICATION_TYPE = "Failure";

        // Act
        var createFinancialAssetServiceResult = await financialAssetService.CreateFinancialAssetServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(createFinancialAssetServiceResult.IsSuccess);
        Assert.NotEmpty(createFinancialAssetServiceResult.Notifications);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_CODE, createFinancialAssetServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_MESSAGE, createFinancialAssetServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_TYPE, createFinancialAssetServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Create_Financial_Asset_Service_Should_Be_Error_When_Any_Param_Is_Error()
    {
        // Arrange
        var financialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        var input = CreateFinancialAssetServiceInput.Factory(
            operatorId: IdentityValueObject.Factory(),
            symbol: "  ",
            description: "   ",
            expirationDate: DateTime.Parse("2023-12-05"),
            index: financialAsset.Index,
            type: financialAsset.Type,
            status: financialAsset.Status,
            interestRate: financialAsset.InterestRate,
            unitaryPrice: financialAsset.UnitaryPrice,
            quantityAvailable: financialAsset.QuantityAvailable);
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        var methodResult = input.GetInputValidationResult();

        // Act
        var createFinancialAssetServiceResult = await financialAssetService.CreateFinancialAssetServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(createFinancialAssetServiceResult.IsSuccess);
        Assert.NotEmpty(createFinancialAssetServiceResult.Notifications);
        Assert.Equal(methodResult.Notifications, createFinancialAssetServiceResult.Notifications);
        Assert.Equal(methodResult.IsError, createFinancialAssetServiceResult.IsError);
    }
}
