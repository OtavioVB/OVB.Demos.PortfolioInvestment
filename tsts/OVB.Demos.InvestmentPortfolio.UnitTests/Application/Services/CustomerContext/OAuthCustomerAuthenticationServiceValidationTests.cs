using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.CustomerContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.CustomerContext;

public sealed class OAuthCustomerAuthenticationServiceValidationTests
{
    [Fact]
    public async void OAuth_Customer_Authentication_Service_Should_Execute_With_Success()
    {
        // Arrange
        var customerService = new CustomerService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            unitOfWork: new FakerUnitOfWork(),
            extensionCustomerRepository: new FakerExtensionCustomerRepository(
                customerExists: true));
        const string EXPECTED_GRANT_TYPE = "password";
        const string EXPECTED_TOKEN_TYPE = "Bearer";
        const int EXPECTED_EXPIRES_IN = 28800;

        var input = OAuthCustomerAuthenticationServiceInput.Factory(
            email: CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Email,
            password: PasswordValueObject.Factory(CustomerDataTransferObjectValidationTests.EXAMPLE_PWD_TEST),
            grantType: EXPECTED_GRANT_TYPE);

        const string EXPECTED_SUCCESS_CODE = "OAUTH_CUSTOMER_AUTHENTICATION_SUCCESS";
        const string EXPECTED_SUCCESS_MESSAGE = "A autenticação de acesso do cliente foi realizada com sucesso.";
        const string EXPECTED_SUCCESS_TYPE = "Success";

        // Act
        var customerServiceResult = await customerService.OAuthCustomerAuthenticationServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(customerServiceResult.IsSuccess);
        Assert.NotEmpty(customerServiceResult.Output.AccessToken);
        Assert.Equal(EXPECTED_GRANT_TYPE, customerServiceResult.Output.GrantType);
        Assert.Equal(EXPECTED_TOKEN_TYPE, customerServiceResult.Output.TokenType);
        Assert.Equal(EXPECTED_EXPIRES_IN, customerServiceResult.Output.ExpiresIn);
        Assert.Equal(EXPECTED_SUCCESS_CODE, customerServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_SUCCESS_MESSAGE, customerServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_SUCCESS_TYPE, customerServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void OAuth_Customer_Authentication_Should_Execute_Without_Success_When_The_Password_Wrong()
    {
        // Arrange
        var customerService = new CustomerService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            unitOfWork: new FakerUnitOfWork(),
            extensionCustomerRepository: new FakerExtensionCustomerRepository(
                customerExists: true));
        const string EXPECTED_GRANT_TYPE = "password";
        const string NOT_VALID_PWD = "KJdsj85#lçx_";

        var input = OAuthCustomerAuthenticationServiceInput.Factory(
            email: CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Email,
            password: PasswordValueObject.Factory(NOT_VALID_PWD),
            grantType: EXPECTED_GRANT_TYPE);

        const string EXPECTED_CODE = "CUSTOMER_CREDENTIALS_IS_NOT_VALID";
        const string EXPECTED_MESSAGE = "As credenciais de cliente enviadas não coincidem.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var customerServiceResult = await customerService.OAuthCustomerAuthenticationServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(customerServiceResult.IsSuccess);
        Assert.Equal(EXPECTED_CODE, customerServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, customerServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, customerServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void OAuth_Customer_Authentication_Should_Execute_Without_Success_When_Any_Param_Is_Not_Valid()
    {
        // Arrange
        var customerService = new CustomerService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            unitOfWork: new FakerUnitOfWork(),
            extensionCustomerRepository: new FakerExtensionCustomerRepository(
                customerExists: false));
        const string EXPECTED_GRANT_TYPE = "password";
        const string NOT_VALID_PWD = "KJdsj85#lçx_";

        var input = OAuthCustomerAuthenticationServiceInput.Factory(
            email: CustomerDataTransferObjectValidationTests.CUSTOMER_EXAMPLE.Email,
            password: PasswordValueObject.Factory(NOT_VALID_PWD),
            grantType: EXPECTED_GRANT_TYPE);

        const string EXPECTED_CODE = "CUSTOMER_NOT_FOUND";
        const string EXPECTED_MESSAGE = "Não foi possível encontrar o cliente.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var customerServiceResult = await customerService.OAuthCustomerAuthenticationServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(customerServiceResult.IsSuccess);
        Assert.Equal(EXPECTED_CODE, customerServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, customerServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, customerServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void OAuth_Customer_Authentication_Should_Execute_Without_Success_When_The_Customer_Not_Found()
    {
        // Arrange
        var customerService = new CustomerService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            unitOfWork: new FakerUnitOfWork(),
            extensionCustomerRepository: new FakerExtensionCustomerRepository(
                customerExists: true));
        const string EXPECTED_GRANT_TYPE = "pard";

        var input = OAuthCustomerAuthenticationServiceInput.Factory(
            email: "invalid_email",
            password: PasswordValueObject.Factory("invalid_passowrd"),
            grantType: EXPECTED_GRANT_TYPE);

        // Act
        var customerServiceResult = await customerService.OAuthCustomerAuthenticationServiceAsync(
            input: input,
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(customerServiceResult.IsSuccess);
        Assert.NotEmpty(customerServiceResult.Notifications);
    }
}
