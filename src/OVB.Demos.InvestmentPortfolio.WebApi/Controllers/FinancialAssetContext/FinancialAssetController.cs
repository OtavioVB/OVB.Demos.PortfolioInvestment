using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
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
    [Route("advices")]
    [AllowAnonymous]
    public async Task<IActionResult> HttpPostAdviceFinancialUpcommingExpirationDateAsync(
        [FromHeader(Name = "X-Administrator-Key")] string administratorKey,
        [FromServices] IFinancialAssetService financialAssetService,
        CancellationToken cancellationToken)
    {
        const string ADMINISTRATOR_KEY = "XLkNQ8h23I73KV8KXw1UrdyiKVhJi7yg";

        if (administratorKey != ADMINISTRATOR_KEY)
            return Unauthorized();

        var serviceResult = await financialAssetService.AdviceFinancialAssetUpcomingExpirationDateAsync(
            cancellationToken: cancellationToken);

        if (serviceResult.IsError)
            return StatusCode(
                statusCode: StatusCodes.Status400BadRequest,
                value: serviceResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: serviceResult.Notifications);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Roles = nameof(Operator))]
    public async Task<IActionResult> HttpPostCreateFinancialAssetAsync(
        [FromServices] IFinancialAssetService financialAssetService,
        [FromBody] CreateFinancialAssetPayloadInput input,
        CancellationToken cancellationToken)
    {
        var operatorId = HttpContext.User.FindFirst("OperatorId")!.Value;

        var createFinancialServiceResult = await financialAssetService.CreateFinancialAssetServiceAsync(
            input: CreateFinancialAssetServiceInput.Factory(
                operatorId: Guid.Parse(operatorId),
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
                quantityAvailable: createFinancialServiceResult.Output.FinancialAsset.QuantityAvailable,
                notifications: createFinancialServiceResult.Notifications));
    }

    [HttpPatch]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("{financialAssetId}")]
    [Authorize(Roles = nameof(Operator))]
    public async Task<IActionResult> HttpPatchUpdateFinancialAssetAsync(
        [FromServices] IFinancialAssetService financialAssetService,
        [FromRoute] Guid financialAssetId,
        [FromBody] UpdateFinancialAssetPayloadInput input,
        CancellationToken cancellationToken)
    {
        var operatorId = HttpContext.User.FindFirst("OperatorId")!.Value;

        var updateFinancialAssetServiceResult = await financialAssetService.UpdateFinancialAssetServiceAsync(
            input: UpdateFinancialAssetServiceInput.Factory(
                operatorId: Guid.Parse(operatorId),
                financialAssetId: financialAssetId,
                symbol: input.Symbol is null ? (AssetSymbolValueObject?)null : AssetSymbolValueObject.Factory(input.Symbol),
                description: DescriptionValueObject.Factory(input.Description),
                expirationDate: input.ExpirationDate is null ? null : input.ExpirationDate,
                status: input.Status is null ? (AssetStatusValueObject?)null : AssetStatusValueObject.Factory(input.Status),
                interestRate: input.InterestRate is null ? null : input.InterestRate.Value,
                unitaryPrice: input.UnitaryPrice is null ? null : input.UnitaryPrice.Value,
                quantityAvailable: input.QuantityAvailable is null ? null : input.QuantityAvailable.Value),
            cancellationToken: cancellationToken);

        if (updateFinancialAssetServiceResult.IsError)
            return BadRequest(updateFinancialAssetServiceResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: UpdateFinancialAssetSendloadOutput.Factory(
                financialAssetId: updateFinancialAssetServiceResult.Output.FinancialAsset.Id,
                symbol: updateFinancialAssetServiceResult.Output.FinancialAsset.Symbol,
                description: updateFinancialAssetServiceResult.Output.FinancialAsset.Description,
                expirationDate: updateFinancialAssetServiceResult.Output.FinancialAsset.ExpirationDate,
                index: updateFinancialAssetServiceResult.Output.FinancialAsset.Index,
                type: updateFinancialAssetServiceResult.Output.FinancialAsset.Type,
                status: updateFinancialAssetServiceResult.Output.FinancialAsset.Status,
                interestRate: updateFinancialAssetServiceResult.Output.FinancialAsset.InterestRate,
                unitaryPrice: updateFinancialAssetServiceResult.Output.FinancialAsset.UnitaryPrice,
                quantityAvailable: updateFinancialAssetServiceResult.Output.FinancialAsset.QuantityAvailable,
                notifications: updateFinancialAssetServiceResult.Notifications));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> HttpGetQueryFinancialAssetAsync(
        [FromServices] IFinancialAssetService financialAssetService,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int size = 25)
    {
        var queryFinancialAssetsServiceResult = await financialAssetService.QueryFinancialAssetServiceAsync(
            input: QueryFinancialAssetServiceInput.Factory(
                page: page,
                offset: size),
            cancellationToken: cancellationToken);

        if (queryFinancialAssetsServiceResult.IsError)
            return StatusCode(
                statusCode: StatusCodes.Status400BadRequest,
                value: queryFinancialAssetsServiceResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status200OK,
            value: QueryFinancialAssetSendloadOutput.Factory(
                page: queryFinancialAssetsServiceResult.Output.Page,
                offset: queryFinancialAssetsServiceResult.Output.Offset,
                items: queryFinancialAssetsServiceResult.Output.FinancialAssets.Select(p => QueryFinancialAssetSendloadOutputItem.Factory(
                    financialAssetId: p.Id,
                    symbol: p.Symbol,
                    description: p.Description,
                    expirationDate: p.ExpirationDate,
                    index: p.Index,
                    type: p.Type,
                    status: p.Status,
                    interestRate: p.InterestRate,
                    unitaryPrice: p.UnitaryPrice,
                    quantityAvailable: p.QuantityAvailable)).ToArray(),
                notifications: queryFinancialAssetsServiceResult.Notifications));
    }
}
