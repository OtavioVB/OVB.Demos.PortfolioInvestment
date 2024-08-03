using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OperatorContext.Fakes;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OperatorContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Application.Services.OperatorContext;

public sealed class OAuthOperatorAuthenticationServiceValidationTests
{
    [Fact]
    public async void OAuth_Operator_Authentication_Service_Will_Be_Successfull()
    {
        // Arrange
        const string GRANT_TYPE = "password";
        const string EXPECTED_TOKEN_TYPE = "Bearer";
        const int EXPECTED_EXPIRES_IN = 3600;
        var operatorService = new OperatorService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            extensionOperatorRepository: FakerExtensionOperatorRepository.Repository);

        const string EXPECTED_SUCCESS_CODE = "OAUTH_OPERATOR_AUTHENTICATION_SUCCESS";
        const string EXPECTED_SUCCESS_MESSAGE = "A autenticação do operador foi realizada com sucesso.";
        const string EXPECTED_SUCCESS_TYPE = "Success";

        // Act
        var operatorServiceResult = await operatorService.OAuthOperatorAuthenticationServiceAsync(
            input: OAuthOperatorAuthenticationServiceInput.Factory(
                grantType: GRANT_TYPE,
                email: OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE.Email,
                password: PasswordValueObject.Factory(OperatorDataTransferObjectValidationTests.EXAMPLE_PWD_TEST)),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.True(operatorServiceResult.IsSuccess);
        Assert.NotEmpty(operatorServiceResult.Output.AccessToken);
        Assert.Equal(GRANT_TYPE, operatorServiceResult.Output.GrantType);
        Assert.Equal(EXPECTED_TOKEN_TYPE, operatorServiceResult.Output.TokenType);
        Assert.Equal(EXPECTED_EXPIRES_IN, operatorServiceResult.Output.ExpiresIn);
        Assert.Equal(EXPECTED_SUCCESS_CODE, operatorServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_SUCCESS_MESSAGE, operatorServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_SUCCESS_TYPE, operatorServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void OAuth_Operator_Authentication_Service_Fail_When_The_Password_Is_Wrong()
    {
        // Arrange
        const string GRANT_TYPE = "password";
        var operatorService = new OperatorService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            extensionOperatorRepository: FakerExtensionOperatorRepository.Repository);

        const string EXPECTED_ERROR_CODE = "OPERATOR_CREDENTIALS_NOT_VALID";
        const string EXPECTED_ERROR_MESSAGE = "As credenciais de operador enviadas não são válidas.";
        const string EXPECTED_ERROR_TYPE = "Failure";

        // Act
        var operatorServiceResult = await operatorService.OAuthOperatorAuthenticationServiceAsync(
            input: OAuthOperatorAuthenticationServiceInput.Factory(
                grantType: GRANT_TYPE,
                email: OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE.Email,
                password: PasswordValueObject.Factory("ijdh55&*lxk")),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(operatorServiceResult.IsSuccess);
        Assert.Equal(EXPECTED_ERROR_CODE, operatorServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_ERROR_MESSAGE, operatorServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_ERROR_TYPE, operatorServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void OAuth_Operator_Authentication_Service_Fail_When_The_Operator_Not_Found_By_Email()
    {
        // Arrange
        const string GRANT_TYPE = "password";
        var operatorService = new OperatorService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            extensionOperatorRepository: FakerExtensionOperatorRepository.Repository);

        const string EXPECTED_ERROR_CODE = "OPERATOR_NOT_FOUND";
        const string EXPECTED_ERROR_MESSAGE = "Não foi possível encontrar o operador para a conta associada.";
        const string EXPECTED_ERROR_TYPE = "Failure";

        // Act
        var operatorServiceResult = await operatorService.OAuthOperatorAuthenticationServiceAsync(
            input: OAuthOperatorAuthenticationServiceInput.Factory(
                grantType: GRANT_TYPE,
                email: "wrong@email.com.br",
                password: PasswordValueObject.Factory(OperatorDataTransferObjectValidationTests.EXAMPLE_PWD_TEST)),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(operatorServiceResult.IsSuccess);
        Assert.Equal(EXPECTED_ERROR_CODE, operatorServiceResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_ERROR_MESSAGE, operatorServiceResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_ERROR_TYPE, operatorServiceResult.Notifications[0].Type);
    }

    [Fact]
    public async void OAuth_Operator_Authentication_Service_Fail_When_Any_Param_Is_Not_Valid()
    {
        // Arrange
        var operatorService = new OperatorService(
            jwtBearerIssuerSigningKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            passwordHashPrivateKey: PasswordValueObjectValidationTests.PRIVATE_KEY,
            extensionOperatorRepository: FakerExtensionOperatorRepository.Repository);

        var email = EmailValueObject.Factory("invalid_email");
        var password = PasswordValueObject.Factory(" ");
        var grantType = GrantTypeValueObject.Factory("  ");

        var EXPECTED_METHOD_RESULT = MethodResult<INotification>.Factory(email, password, grantType);

        // Act
        var operatorServiceResult = await operatorService.OAuthOperatorAuthenticationServiceAsync(
            input: OAuthOperatorAuthenticationServiceInput.Factory(
                grantType: grantType,
                email: email,
                password: password),
            cancellationToken: CancellationToken.None);

        // Assert
        Assert.False(operatorServiceResult.IsSuccess);
        Assert.All(EXPECTED_METHOD_RESULT.Notifications, item => Assert.Contains(item, operatorServiceResult.Notifications));
    }
}
