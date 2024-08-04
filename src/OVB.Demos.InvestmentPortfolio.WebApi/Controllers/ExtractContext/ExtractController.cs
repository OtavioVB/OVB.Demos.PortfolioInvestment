using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.ExtractContext.Sendloads;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.ExtractContext;

[Route("api/v1/financial-assets")]
[ApiController]
public sealed class ExtractController : ControllerBase
{
    [HttpGet]
    [Route("extracts")]
    [Authorize(Roles = nameof(Customer))]
    public async Task<IActionResult> HttpGetQueryFinancialAssetsExtractAsync(
        [FromServices] IExtractService extractService,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int size = 25)
    {
        var customerId = Guid.Parse(HttpContext.User.FindFirst("CustomerId")!.Value);

        var serviceResult = await extractService.QueryExtractServiceAsync(
            input: QueryExtractServiceInput.Factory(
                customerId: customerId,
                page: page,
                offset: size),
            cancellationToken: cancellationToken);

        if (serviceResult.IsError)
            return StatusCode(
                statusCode: StatusCodes.Status400BadRequest,
                value: serviceResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: QueryExtractSendloadOutput.Factory(
                page: serviceResult.Output.Page,
                offset: serviceResult.Output.Offset,
                items: serviceResult.Output.Items.Select(p => QueryExtractSendloadOutputItem.Factory(
                    extractId: p.Id,
                    createdAt: p.CreatedAt,
                    type: p.Type,
                    totalPrice: p.TotalPrice,
                    unitaryPrice: p.UnitaryPrice,
                    quantity: p.Quantity,
                    financialAsset: QueryExtractSendloadOutputItemFinancialAsset.Factory(
                        financialAssetId: p.FinancialAsset!.Id,
                        symbol: p.FinancialAsset.Symbol,
                        description: p.FinancialAsset.Description,
                        expirationDate: p.FinancialAsset.ExpirationDate,
                        index: p.FinancialAsset.Index,
                        type: p.FinancialAsset.Type,
                        status: p.FinancialAsset.Status,
                        interestRate: p.FinancialAsset.InterestRate,
                        unitaryPrice: p.FinancialAsset.UnitaryPrice,
                        quantityAvailable: p.FinancialAsset.QuantityAvailable))).ToArray(),
                notifications: serviceResult.Notifications));
    }
}
