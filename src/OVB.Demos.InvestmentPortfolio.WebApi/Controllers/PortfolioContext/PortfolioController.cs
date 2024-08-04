using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.WebApi.Controllers.PortfolioContext.Sendloads;
using System.Net.Mime;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.PortfolioContext;

[Route("api/v1/financial-assets")]
[ApiController]
public sealed class PortfolioController : ControllerBase
{
    [HttpGet]
    [Route("portfolios")]
    [Authorize(Roles = nameof(Customer))]
    public async Task<IActionResult> HttpGetFinancialAssetPortfolioAsync(
        [FromServices] IPortfolioService portfolioService,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int size = 25)
    {
        var customerId = Guid.Parse(HttpContext.User.FindFirst("CustomerId")!.Value);

        var serviceResult = await portfolioService.QueryPortfolioServiceAsync(
            input: QueryPortfolioServiceInput.Factory(
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
            value: QueryPortfoliosSendloadOutput.Factory(
                page: serviceResult.Output.Page,
                offset: serviceResult.Output.Offset,
                items: serviceResult.Output.Items.Select(p => QueryPortfoliosSendloadOutputItem.Factory(
                    portfolioId: p.Id,
                    totalPrice: p.TotalPrice,
                    quantity: p.Quantity,
                    financialAsset: QueryPortfoliosSendloadOutputItemFinancialAsset.Factory(
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
