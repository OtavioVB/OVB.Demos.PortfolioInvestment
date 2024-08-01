using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers;

[Route("api/v1/financial-assets")]
[ApiController]
public sealed class OrderController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("{financialAsset}/orders")]
    [AllowAnonymous]
    public async Task<IActionResult> HttpPostCreateOrderAsync(CancellationToken cancellationToken)
        => Ok(string.Empty);
}
