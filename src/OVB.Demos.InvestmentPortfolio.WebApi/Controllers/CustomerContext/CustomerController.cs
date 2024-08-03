using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OperatorContext.Sendloads;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.CustomerContext;

[Route("api/v1/customers")]
[ApiController]
public sealed class CustomerController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("oauth/token")]
    [AllowAnonymous]
    public async Task<IActionResult> HttpPostOAuthCustomerAuthenticationAsync(
        [FromServices] ICustomerService customerService,
        [Required][FromForm] string email,
        [Required][FromForm] string password,
        [Required][FromForm] string grantType,
        CancellationToken cancellationToken)
    {
        var oauthCustomerServiceResult = await customerService.OAuthCustomerAuthenticationServiceAsync(
            input: OAuthCustomerAuthenticationServiceInput.Factory(
                email: email,
                password: PasswordValueObject.Factory(password),
                grantType: grantType),
            cancellationToken: cancellationToken);

        if (oauthCustomerServiceResult.IsError)
            return StatusCode(
                statusCode: StatusCodes.Status400BadRequest,
                value: oauthCustomerServiceResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: OAuthCustomerAuthenticationSendloadOutput.Factory(
                accessToken: oauthCustomerServiceResult.Output.AccessToken,
                tokenType: oauthCustomerServiceResult.Output.TokenType,
                expiresIn: oauthCustomerServiceResult.Output.ExpiresIn,
                grantType: oauthCustomerServiceResult.Output.GrantType,
                notifications: oauthCustomerServiceResult.Notifications));
    }
}
