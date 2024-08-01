using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers;

[Route("api/v1/customers")]
[ApiController]
public sealed class CustomerController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [AllowAnonymous]
    public async Task<IActionResult> HttpPostCreateCustomerAsync(CancellationToken cancellationToken)
        => Ok(string.Empty);
}
