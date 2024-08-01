using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [FromForm] string email,
        [FromForm] string password,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
