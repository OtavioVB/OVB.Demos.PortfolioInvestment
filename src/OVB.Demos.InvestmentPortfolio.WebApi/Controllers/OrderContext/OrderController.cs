using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OrderContext.Payloads;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OrderContext.Sendloads;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OrderContext;

[Route("api/v1/financial-assets")]
[ApiController]
public sealed class OrderController : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("{financialAssetId}/orders/{orderType}")]
    [Authorize(Roles = nameof(Customer))]
    public async Task<IActionResult> HttpPostCreateOrderAsync(
        [FromServices] IUseCase<CreateOrderUseCaseInput, CreateOrderUseCaseOutput> useCase,
        [FromRoute] Guid financialAssetId,
        [FromRoute] string orderType,
        [FromBody] CreateOrderPayloadInput input,
        CancellationToken cancellationToken)
    {
        var customerId = HttpContext.User.FindFirst("CustomerId")!.Value;

        var useCaseResult = await useCase.ExecuteUseCaseOrchestratorAsync(
            input: CreateOrderUseCaseInput.Factory(
                customerId: Guid.Parse(customerId),
                financialAssetId: financialAssetId,
                type: orderType,
                quantity: input.Quantity),
            cancellationToken: cancellationToken);

        if (useCaseResult.IsError)
            return StatusCode(
                statusCode: StatusCodes.Status400BadRequest,
                value: useCaseResult.Notifications);

        return StatusCode(
            statusCode: StatusCodes.Status201Created,
            value: CreateOrderSendloadOutput.Factory(
                orderId: useCaseResult.Output.Order.Id,
                createdAt: useCaseResult.Output.Order.CreatedAt.ToString(),
                type: useCaseResult.Output.Order.Type,
                status: useCaseResult.Output.Order.Status,
                quantity: useCaseResult.Output.Order.Quantity,
                unitaryPrice: useCaseResult.Output.Order.UnitaryPrice,
                totalPrice: useCaseResult.Output.Order.TotalPrice,
                financialAsset: CreateOrderSendloadOutputFinancialAsset.Factory(
                    financialAssetId: useCaseResult.Output.FinancialAsset.Id,
                    symbol: useCaseResult.Output.FinancialAsset.Symbol,
                    description: useCaseResult.Output.FinancialAsset.Description,
                    expirationDate: useCaseResult.Output.FinancialAsset.ExpirationDate,
                    index: useCaseResult.Output.FinancialAsset.Index,
                    type: useCaseResult.Output.FinancialAsset.Type,
                    status: useCaseResult.Output.FinancialAsset.Status,
                    interestRate: useCaseResult.Output.FinancialAsset.InterestRate,
                    unitaryPrice: useCaseResult.Output.FinancialAsset.UnitaryPrice,
                    quantityAvailable: useCaseResult.Output.FinancialAsset.QuantityAvailable),
                notifications: useCaseResult.Notifications));
    }
}
