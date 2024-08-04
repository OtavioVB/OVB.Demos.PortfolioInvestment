using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.FinancialAssetContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OrderContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OrderContext;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OrderContext;

public sealed class CreateOrderServiceValidationTests
{
    [Fact]
    public async void Create_Order_Sell_Service_Should_Result_Success_As_Expected()
    {
        // Arrange
        const string ORDER_TYPE = "SELL";

        var repositoryFinancialAsset = new FakerExtensionFinancialAssetRepository(
            existsSymbol: false,
            existsFinancialAssetOnGet: true,
            financialAssetQuantityBuyIsGreaterThanTheAllowed: true);

        var orderService = new OrderService(
            orderBaseRepository: new FakerOrderRepository(),
            extensionFinancialAssetRepository: repositoryFinancialAsset,
            unitOfWork: new FakerUnitOfWork());

        const string EXPECTED_NOTIFICATION_CODE = "CREATE_ORDER_SELL_DONE";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A criação da ordem de venda do ativo financeiro foi realizada com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await orderService.CreateOrderServiceAsync(
            input: CreateOrderServiceInput.Factory(
                customerId: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId,
                type: ORDER_TYPE,
                quantity: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity,
                financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.Single(serviceResult.Notifications);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Create_Order_Buy_Service_Should_Result_Success_As_Expected()
    {
        // Arrange
        const string ORDER_TYPE = "BUY";

        var repositoryFinancialAsset = new FakerExtensionFinancialAssetRepository(
            existsSymbol: false,
            existsFinancialAssetOnGet: true,
            financialAssetQuantityBuyIsGreaterThanTheAllowed: true);

        var orderService = new OrderService(
            orderBaseRepository: new FakerOrderRepository(),
            extensionFinancialAssetRepository: repositoryFinancialAsset,
            unitOfWork: new FakerUnitOfWork());

        const string EXPECTED_NOTIFICATION_CODE = "CREATE_ORDER_BUY_DONE";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A criação da ordem de compra do ativo financeiro foi realizada com sucesso.";
        const string EXPECTED_NOTIFICATION_TYPE = "Success";

        // Act
        var serviceResult = await orderService.CreateOrderServiceAsync(
            input: CreateOrderServiceInput.Factory(
                customerId: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId,
                type: ORDER_TYPE,
                quantity: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity,
                financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(serviceResult.IsSuccess);
        Assert.Single(serviceResult.Notifications);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Create_Order_Buy_Service_Should_Result_Error_When_Quantity_Allowed_Is_Less_Than_The_Required_Quantity_By_Order()
    {
        // Arrange
        const string ORDER_TYPE = "BUY";

        var repositoryFinancialAsset = new FakerExtensionFinancialAssetRepository(
            existsSymbol: false,
            existsFinancialAssetOnGet: true,
            financialAssetQuantityBuyIsGreaterThanTheAllowed: false);

        var orderService = new OrderService(
            orderBaseRepository: new FakerOrderRepository(),
            extensionFinancialAssetRepository: repositoryFinancialAsset,
            unitOfWork: new FakerUnitOfWork());

        const string EXPECTED_NOTIFICATION_CODE = "CREATE_ORDER_BUY_CANNOT_BE_DONE_BECAUSE_DOESNT_HAS_ASSET_QUANTITY_AVAILABLE";
        const string EXPECTED_NOTIFICATION_MESSAGE = "A criação da ordem de compra não pode ser realizada, pois a quantidade de ativo financeiro é menor que a quantidade solicitada.";
        const string EXPECTED_NOTIFICATION_TYPE = "Failure";

        // Act
        var serviceResult = await orderService.CreateOrderServiceAsync(
            input: CreateOrderServiceInput.Factory(
                customerId: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId,
                type: ORDER_TYPE,
                quantity: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.Quantity,
                financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(serviceResult.IsSuccess);
        Assert.Single(serviceResult.Notifications);
        Assert.Equal(EXPECTED_NOTIFICATION_CODE, serviceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_NOTIFICATION_MESSAGE, serviceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_NOTIFICATION_TYPE, serviceResult.Notifications[0].Type);
    }

    [Fact]
    public async void Create_Order_Buy_Service_Should_Result_Error_When_Any_Param_Send_Is_Invalid()
    {
        // Arrange
        const string ORDER_TYPE = "INVALID_PARAM";

        var repositoryFinancialAsset = new FakerExtensionFinancialAssetRepository(
            existsSymbol: false,
            existsFinancialAssetOnGet: true,
            financialAssetQuantityBuyIsGreaterThanTheAllowed: false);

        var orderService = new OrderService(
            orderBaseRepository: new FakerOrderRepository(),
            extensionFinancialAssetRepository: repositoryFinancialAsset,
            unitOfWork: new FakerUnitOfWork());

        var customerId = OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId;
        OrderTypeValueObject orderType = ORDER_TYPE;
        QuantityValueObject quantity = 0;

        var expectedMethodResult = MethodResult<INotification>.Factory(customerId, orderType, quantity);

        // Act
        var serviceResult = await orderService.CreateOrderServiceAsync(
            input: CreateOrderServiceInput.Factory(
                customerId: OrderDataTransferObjectValidationTests.ORDER_EXAMPLE.CustomerId,
                type: ORDER_TYPE,
                quantity: quantity,
                financialAsset: FinancialAssetDataTransferObjectValidationTests.FINANCIAL_ASSET_EXAMPLE),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(serviceResult.IsSuccess);
        Assert.NotEmpty(serviceResult.Notifications);
        Assert.Equal(expectedMethodResult.Notifications, serviceResult.Notifications);
    }
}
