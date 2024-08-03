using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;
using System.Globalization;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext;

public sealed class UpdateFinancialAssetServiceValidationTests
{
    [Fact]
    public async void Update_Financial_Asset_Service_Should_Be_Created_Financial_Asset()
    {
        // Arrange
        var financialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        var input = UpdateFinancialAssetServiceInput.Factory(
            operatorId: IdentityValueObject.Factory(),
            financialAssetId: IdentityValueObject.Factory(),
            symbol: financialAsset.Symbol,
            description: financialAsset.Description!.Value,
            expirationDate: financialAsset.ExpirationDate,
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

        const string EXPECTED_SUCCESS_NOTIFICATION_CODE = "UPDATE_FINANCIAL_ASSET_SUCCESS";
        string EXPECTED_SUCCESS_NOTIFICATION_MESSAGE = string.Format("O ativo financeiro {0} foi atualizado com sucesso.", input.Symbol!.Value.GetSymbol());
        const string EXPECTED_SUCCESS_NOTIFICATION_TYPE = "Success";

        // Act
        var updateFinancialAssetServiceResult = await financialAssetService.UpdateFinancialAssetServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(updateFinancialAssetServiceResult.IsSuccess);
        Assert.NotEmpty(updateFinancialAssetServiceResult.Notifications);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_CODE, updateFinancialAssetServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_MESSAGE, updateFinancialAssetServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_SUCCESS_NOTIFICATION_TYPE, updateFinancialAssetServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Update_Financial_Asset_Service_Should_Be_Error_When_Financial_Asset_Not_Found()
    {
        // Arrange
        var financialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        var input = UpdateFinancialAssetServiceInput.Factory(
            operatorId: IdentityValueObject.Factory(),
            financialAssetId: IdentityValueObject.Factory(),
            symbol: financialAsset.Symbol,
            description: financialAsset.Description!.Value,
            expirationDate: financialAsset.ExpirationDate,
            status: financialAsset.Status,
            interestRate: financialAsset.InterestRate,
            unitaryPrice: financialAsset.UnitaryPrice,
            quantityAvailable: financialAsset.QuantityAvailable);
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: false,
                existsFinancialAssetOnGet: false),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_ERROR_NOTIFICATION_CODE = "FINANCIAL_ASSET_NOT_FOUND";
        const string EXPECTED_ERROR_NOTIFICATION_MESSAGE = "O ativo financeiro enviado não foi possível ser encontrado.";
        const string EXPECTED_ERROR_NOTIFICATION_TYPE = "Failure";

        // Act
        var updateFinancialAssetServiceResult = await financialAssetService.UpdateFinancialAssetServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(updateFinancialAssetServiceResult.IsSuccess);
        Assert.NotEmpty(updateFinancialAssetServiceResult.Notifications);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_CODE, updateFinancialAssetServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_MESSAGE, updateFinancialAssetServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_TYPE, updateFinancialAssetServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Update_Financial_Asset_Service_Should_Be_Error_When_Financial_Asset_Already_Exists_By_Symbol()
    {
        // Arrange
        var financialAsset = FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE;

        var input = UpdateFinancialAssetServiceInput.Factory(
            operatorId: IdentityValueObject.Factory(),
            financialAssetId: IdentityValueObject.Factory(),
            symbol: "NTIDH",
            description: financialAsset.Description!.Value,
            expirationDate: financialAsset.ExpirationDate,
            status: financialAsset.Status,
            interestRate: financialAsset.InterestRate,
            unitaryPrice: financialAsset.UnitaryPrice,
            quantityAvailable: financialAsset.QuantityAvailable);
        var financialAssetService = new FinancialAssetService(
            extensionFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: true,
                existsFinancialAssetOnGet: true),
            baseFinancialAssetRepository: new FakerExtensionFinancialAssetRepository(
                existsSymbol: true,
                existsFinancialAssetOnGet: true),
            unitOfWork: new FakerUnitOfWork(),
            sendEmailApiKey: string.Empty);

        const string EXPECTED_ERROR_NOTIFICATION_CODE = "FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL";
        const string EXPECTED_ERROR_NOTIFICATION_MESSAGE = "O ativo financeiro enviado já existe para o símbolo associado enviado.";
        const string EXPECTED_ERROR_NOTIFICATION_TYPE = "Failure";

        // Act
        var updateFinancialAssetServiceResult = await financialAssetService.UpdateFinancialAssetServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(updateFinancialAssetServiceResult.IsSuccess);
        Assert.NotEmpty(updateFinancialAssetServiceResult.Notifications);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_CODE, updateFinancialAssetServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_MESSAGE, updateFinancialAssetServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_ERROR_NOTIFICATION_TYPE, updateFinancialAssetServiceResult.Notifications[0].Type);
    }
}
