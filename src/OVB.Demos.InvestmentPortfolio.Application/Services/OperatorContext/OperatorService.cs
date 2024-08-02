using Microsoft.IdentityModel.Tokens;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext;

public sealed class OperatorService : IOperatorService
{
    private readonly string _jwtBearerIssuerSigningKey;
    private readonly string _passwordHashPrivateKey;
    private readonly IExtensionOperatorRepository _extensionOperatorRepository;

    public OperatorService(
        string jwtBearerIssuerSigningKey,
        string passwordHashPrivateKey, 
        IExtensionOperatorRepository extensionOperatorRepository)
    {
        _jwtBearerIssuerSigningKey = jwtBearerIssuerSigningKey;
        _passwordHashPrivateKey = passwordHashPrivateKey;
        _extensionOperatorRepository = extensionOperatorRepository;
    }

    public async Task<MethodResult<INotification, OAuthOperatorAuthenticationServiceOutput>> OAuthOperatorAuthenticationServiceAsync(
        OAuthOperatorAuthenticationServiceInput input, 
        CancellationToken cancellationToken)
    {
        MethodResult<INotification> inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, OAuthOperatorAuthenticationServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var queryOperatorResult = await _extensionOperatorRepository.GetOperatorByEmailAsNoTrackingAsync(
            email: input.Email,
            cancellationToken: cancellationToken);

        var operatorNotFoundValidationResult = ValidateOperatorNotFound(queryOperatorResult);
        
        if (operatorNotFoundValidationResult.IsError)
            return MethodResult<INotification, OAuthOperatorAuthenticationServiceOutput>.FactoryError(
                notifications: operatorNotFoundValidationResult.Notifications);

        var operatorCredentialsValidationResult = ValidateExpectedPasswordWithActual(
            expectedPassword: queryOperatorResult!.PasswordHash,
            actualPassword: input.Password.GetPasswordHashAndSalt(
                privateKey: _passwordHashPrivateKey,
                alreadyDefinedSalt: queryOperatorResult.Salt).PasswordHash);

        if (operatorCredentialsValidationResult.IsError)
            return MethodResult<INotification, OAuthOperatorAuthenticationServiceOutput>.FactoryError(
                notifications: operatorCredentialsValidationResult.Notifications);

        var operatorAuthenticationResult = AuthenticateOperator(
            operatorId: queryOperatorResult.Id,
            grantType: input.GrantType,
            operatorCode: queryOperatorResult.Code,
            email: queryOperatorResult.Email);

        const string OAUTH_OPERATOR_AUTHENTICATION_SUCCESS_NOTIFICATION_CODE = "OAUTH_OPERATOR_AUTHENTICATION_SUCCESS";
        const string OAUTH_OPERATOR_AUTHENTICATION_SUCCESS_NOTIFICATION_MESSAGE = "A autenticação do operador foi realizada com sucesso.";

        return MethodResult<INotification, OAuthOperatorAuthenticationServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: OAUTH_OPERATOR_AUTHENTICATION_SUCCESS_NOTIFICATION_CODE,
                message: OAUTH_OPERATOR_AUTHENTICATION_SUCCESS_NOTIFICATION_MESSAGE)],
            output: OAuthOperatorAuthenticationServiceOutput.Factory(
                tokenType: operatorAuthenticationResult.TokenType,
                accessToken: operatorAuthenticationResult.AccessToken,
                grantType: input.GrantType,
                expiresIn: operatorAuthenticationResult.ExpiresIn));
    }

    private (string AccessToken, int ExpiresIn, string TokenType) AuthenticateOperator(
        IdentityValueObject operatorId,
        GrantTypeValueObject grantType,
        CodeValueObject operatorCode,
        EmailValueObject email)
    {
        const int EXPIRES_IN_DEFAULT = 3600;
        const string TOKEN_TYPE = "Bearer";

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(
                    type: ClaimTypes.Role,
                    value: nameof(Operator)),
                new Claim(
                    type: "OperatorId",
                    value: operatorId.GetIdentityAsString()),
                new Claim(
                    type: "GrantType",
                    value: grantType.GetGrantType()),
                new Claim(
                    type: "Code",
                    value: operatorCode.GetCode()),
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

    private static MethodResult<INotification> ValidateExpectedPasswordWithActual(
        string expectedPassword,
        string actualPassword)
    {
        const string OPERATOR_CREDENTIALS_NOT_VALID_NOTIFICATION_CODE = "OPERATOR_CREDENTIALS_NOT_VALID";
        const string OPERATOR_CREDENTIALS_NOT_VALID_NOTIFICATION_MESSAGE = "As credenciais de operador enviadas não são válidas.";

        if (expectedPassword != actualPassword)
            return MethodResult<INotification>.FactoryError(
                notifications: [Notification.FactoryFailure(
                    code: OPERATOR_CREDENTIALS_NOT_VALID_NOTIFICATION_CODE,
                    message: OPERATOR_CREDENTIALS_NOT_VALID_NOTIFICATION_MESSAGE)]);

        return MethodResult<INotification>.FactorySuccess();
    }

    private static MethodResult<INotification> ValidateOperatorNotFound(Operator? queriedOperator)
    {
        const string OPERATOR_NOT_FOUND_NOTIFICATION_CODE = "OPERATOR_NOT_FOUND";
        const string OPERATOR_NOT_FOUND_NOTIFICATION_MESSAGE = "Não foi possível encontrar o operador para a conta associada.";

        if (queriedOperator is null)
            return MethodResult<INotification>.FactoryError(
                notifications: [Notification.FactoryFailure(
                    code: OPERATOR_NOT_FOUND_NOTIFICATION_CODE,
                    message: OPERATOR_NOT_FOUND_NOTIFICATION_MESSAGE)]);

        return MethodResult<INotification>.FactorySuccess();
    }
}
