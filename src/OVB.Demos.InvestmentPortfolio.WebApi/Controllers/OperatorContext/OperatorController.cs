using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OperatorContext.Sendloads;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OperatorContext;

[Route("api/v1/operators")]
[ApiController]
public sealed class OperatorController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [Route("oauth/token")]
    [AllowAnonymous]
    public async Task<IActionResult> HttpPostOAuthOperatorAuthenticationAsync(
        [FromServices] IOperatorService operatorService,
        [MaxLength(EmailValueObject.MAX_LENGTH)][Required][FromForm] string email,
        [MaxLength(PasswordValueObject.MAX_LENGTH)][Required][FromForm] string password,
        [Required][FromForm] string grantType,
        CancellationToken cancellationToken)
    {
        var oauthOperatorServiceResult = await operatorService.OAuthOperatorAuthenticationServiceAsync(
            input: OAuthOperatorAuthenticationServiceInput.Factory(
                grantType: grantType,
                email: email,
                password: PasswordValueObject.Factory(password, isEncrypted: false)),
            cancellationToken: cancellationToken);

        if (oauthOperatorServiceResult.IsError)
            return BadRequest(oauthOperatorServiceResult.Notifications);

        return Ok(
            value: OAuthOperatorAuthenticationSendloadOutput.Factory(
                accessToken: oauthOperatorServiceResult.Output.AccessToken,
                tokenType: oauthOperatorServiceResult.Output.TokenType,
                expiresIn: oauthOperatorServiceResult.Output.ExpiresIn,
                grantType: oauthOperatorServiceResult.Output.GrantType,
                notifications: oauthOperatorServiceResult.Notifications));
    }
}
