using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers;

[Route("api/v1/operators")]
[ApiController]
public sealed class OperatorController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    [Route("oauth/token")]
    [AllowAnonymous]
    public Task<IActionResult> HttpPostOAuthOperatorAuthenticationAsync(
        [MaxLength(EmailValueObject.MAX_LENGTH)][Required][FromForm] string email,
        [MaxLength(PasswordValueObject.MAX_LENGTH)][Required][FromForm] string password,
        [Required][FromForm] string grantType,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
