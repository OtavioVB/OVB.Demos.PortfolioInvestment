using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers;

[Route("api/v1/financial-assets")]
[ApiController]
public sealed class FinancialAssetController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [AllowAnonymous]
    public async Task<IActionResult> HttpPostCreateFinancialAssetAsync(CancellationToken cancellationToken)
        => Ok(string.Empty);
}
