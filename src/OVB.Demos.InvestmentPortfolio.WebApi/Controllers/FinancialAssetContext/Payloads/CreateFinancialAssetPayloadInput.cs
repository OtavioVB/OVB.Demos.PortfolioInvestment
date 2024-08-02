using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Payloads;

public readonly struct CreateFinancialAssetPayloadInput
{
    public CreateFinancialAssetPayloadInput(string symbol, string? description, DateTime expirationDate, string index, string type, string status, 
        decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
    {
        Symbol = symbol;
        Description = description;
        ExpirationDate = expirationDate;
        Index = index;
        Type = type;
        Status = status;
        InterestRate = interestRate;
        UnitaryPrice = unitaryPrice;
        QuantityAvailable = quantityAvailable;
    }

    public string Symbol { get; init; }
    public string? Description { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string Index { get; init; }
    public string Type { get; init; }
    public string Status { get; init; }
    public decimal InterestRate { get; init; }
    public decimal UnitaryPrice { get; init; }
    public decimal QuantityAvailable { get; init; }
}
