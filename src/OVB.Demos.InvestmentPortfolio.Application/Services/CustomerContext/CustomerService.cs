using Microsoft.IdentityModel.Tokens;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext;

public sealed class CustomerService : ICustomerService
{
    private readonly string _jwtBearerIssuerSigningKey;
    private readonly string _passwordHashPrivateKey;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExtensionCustomerRepository _extensionCustomerRepository;

    public CustomerService(
        string jwtBearerIssuerSigningKey,
        string passwordHashPrivateKey,
        IUnitOfWork unitOfWork, 
        IExtensionCustomerRepository extensionCustomerRepository)
    {
        _jwtBearerIssuerSigningKey = jwtBearerIssuerSigningKey;
        _passwordHashPrivateKey = passwordHashPrivateKey;
        _unitOfWork = unitOfWork;
        _extensionCustomerRepository = extensionCustomerRepository;
    }

    public async Task<MethodResult<INotification, OAuthCustomerAuthenticationServiceOutput>> OAuthCustomerAuthenticationServiceAsync(
        OAuthCustomerAuthenticationServiceInput input, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, OAuthCustomerAuthenticationServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var queryCustomerResult = await _extensionCustomerRepository.GetCustomerByEmailAsNoTrackingAsync(
            email: input.Email,
            cancellationToken: cancellationToken);

        var customerNotFoundValidationResult = ValidateCustomerNotFoundOnDatabase(queryCustomerResult);

        if (customerNotFoundValidationResult.IsError)
            return MethodResult<INotification, OAuthCustomerAuthenticationServiceOutput>.FactoryError(
                notifications: customerNotFoundValidationResult.Notifications);

        var customer = queryCustomerResult;

        var customerExpectedPasswordCredentialsValidationResult = ValidateCustomerExpectedPasswordCredentials(
            expectedPasswordHash: customer!.PasswordHash,
            actualPasswordHash: input.Password.GetPasswordHashAndSalt(
                privateKey: _passwordHashPrivateKey,
                alreadyDefinedSalt: customer.Salt).PasswordHash);

        if (customerExpectedPasswordCredentialsValidationResult.IsError)
            return MethodResult<INotification, OAuthCustomerAuthenticationServiceOutput>.FactoryError(
                notifications: customerExpectedPasswordCredentialsValidationResult.Notifications);

        var customerAuthentication = AuthenticateCustomer(
            customerId: customer.Id,
            grantType: input.GrantType,
            customerCode: customer.Code,
            email: customer.Email);

        const string OAUTH_CUSTOMER_AUTHENTICATION_SUCCESS_NOTIFICATION_CODE = "OAUTH_CUSTOMER_AUTHENTICATION_SUCCESS";
        const string OAUTH_CUSTOMER_AUTHENTICATION_SUCCESS_NOTIFICATION_MESSAGE = "A autenticação de acesso do cliente foi realizada com sucesso.";

        return MethodResult<INotification, OAuthCustomerAuthenticationServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: OAUTH_CUSTOMER_AUTHENTICATION_SUCCESS_NOTIFICATION_CODE,
                message: OAUTH_CUSTOMER_AUTHENTICATION_SUCCESS_NOTIFICATION_MESSAGE)],
            output: OAuthCustomerAuthenticationServiceOutput.Factory(
                tokenType: customerAuthentication.TokenType,
                accessToken: customerAuthentication.AccessToken,
                grantType: input.GrantType,
                expiresIn: customerAuthentication.ExpiresIn));
    }

    private (string AccessToken, int ExpiresIn, string TokenType) AuthenticateCustomer(
        IdentityValueObject customerId,
        GrantTypeValueObject grantType,
        CodeValueObject customerCode,
        EmailValueObject email)
    {
        const int EXPIRES_IN_DEFAULT = 28800;
        const string TOKEN_TYPE = "Bearer";

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(
                    type: ClaimTypes.Role,
                    value: nameof(Customer)),
                new Claim(
                    type: "CustomerId",
                    value: customerId.GetIdentityAsString()),
                new Claim(
                    type: "GrantType",
                    value: grantType.GetGrantType()),
                new Claim(
                    type: "Code",
                    value: customerCode.GetCode()),
                new Claim(
                    type: "Email",
                    value: email)
            }),
            Expires = DateTime.UtcNow.AddSeconds(EXPIRES_IN_DEFAULT),
            SigningCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(
                    key: Encoding.UTF8.GetBytes(_jwtBearerIssuerSigningKey)),
                algorithm: SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (
            AccessToken: tokenHandler.WriteToken(token),
            ExpiresIn: EXPIRES_IN_DEFAULT,
            TokenType: TOKEN_TYPE);
    }

    private static MethodResult<INotification> ValidateCustomerExpectedPasswordCredentials(
        string expectedPasswordHash,
        string actualPasswordHash)
    {
        const string CUSTOMER_CREDENTIALS_IS_NOT_VALID_NOTIFICATION_CODE = "CUSTOMER_CREDENTIALS_IS_NOT_VALID";
        const string CUSTOMER_CREDENTIALS_IS_NOT_VALID_NOTIFICATION_MESSAGE = "As credenciais de cliente enviadas não coincidem.";

        if (expectedPasswordHash != actualPasswordHash)
            return MethodResult<INotification>.FactoryError(
                notifications: [Notification.FactoryFailure(
                    code: CUSTOMER_CREDENTIALS_IS_NOT_VALID_NOTIFICATION_CODE,
                    message: CUSTOMER_CREDENTIALS_IS_NOT_VALID_NOTIFICATION_MESSAGE)]);

        return MethodResult<INotification>.FactorySuccess();
    }

    private static MethodResult<INotification> ValidateCustomerNotFoundOnDatabase(Customer? customer)
    {
        const string CUSTOMER_NOT_FOUND_NOTIFICATION_CODE = "CUSTOMER_NOT_FOUND";
        const string CUSTOMER_NOT_FOUND_NOTIFICATION_MESSAGE = "Não foi possível encontrar o cliente.";

        if (customer is null)
            return MethodResult<INotification>.FactoryError(
                notifications: [Notification.FactoryFailure(
                    code: CUSTOMER_NOT_FOUND_NOTIFICATION_CODE,
                    message: CUSTOMER_NOT_FOUND_NOTIFICATION_MESSAGE)]);

        return MethodResult<INotification>.FactorySuccess();
    }
}
