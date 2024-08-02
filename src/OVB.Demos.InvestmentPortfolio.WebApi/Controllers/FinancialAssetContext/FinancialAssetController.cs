using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Payloads;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Sendloads;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext;

[Route("api/v1/financial-assets")]
[ApiController]
public sealed class FinancialAssetController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Roles = nameof(Operator))]
    public async Task<IActionResult> HttpPostCreateFinancialAssetAsync(
        [FromServices] IFinancialAssetService financialAssetService,
        [FromBody] CreateFinancialAssetPayloadInput input,
        CancellationToken cancellationToken)
    {
        var createFinancialServiceResult = await financialAssetService.CreateFinancialAssetServiceAsync(
            input: CreateFinancialAssetServiceInput.Factory(
                operatorId: Guid.Empty,
                symbol: input.Symbol,
                description: input.Description,
                expirationDate: input.ExpirationDate,
                index: input.Index,
                type: input.Type,
                status: input.Status,
                interestRate: input.InterestRate,
                unitaryPrice: input.UnitaryPrice,
                quantityAvailable: input.QuantityAvailable),
            cancellationToken: cancellationToken);

        if (createFinancialServiceResult.IsError)
            return BadRequest(createFinancialServiceResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status201Created,
            value: CreateFinancialAssetSendloadOutput.Factory(
                financialId: createFinancialServiceResult.Output.FinancialAsset.Id,
                symbol: createFinancialServiceResult.Output.FinancialAsset.Symbol,
                description: createFinancialServiceResult.Output.FinancialAsset.Description,
                expirationDate: createFinancialServiceResult.Output.FinancialAsset.ExpirationDate,
                index: createFinancialServiceResult.Output.FinancialAsset.Index,
                type: createFinancialServiceResult.Output.FinancialAsset.Type,
                status: createFinancialServiceResult.Output.FinancialAsset.Status,
                interestRate: createFinancialServiceResult.Output.FinancialAsset.InterestRate,
                unitaryPrice: createFinancialServiceResult.Output.FinancialAsset.UnitaryPrice,
                quantityAvailable: createFinancialServiceResult.Output.FinancialAsset.QuantityAvailable));
    }

    [HttpPatch]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Roles = nameof(Operator))]
    public async Task<IActionResult> HttpPostEditFinancialAssetAsync(
        [FromServices] IFinancialAssetService financialAssetService,
        [FromBody] CreateFinancialAssetPayloadInput input,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}
