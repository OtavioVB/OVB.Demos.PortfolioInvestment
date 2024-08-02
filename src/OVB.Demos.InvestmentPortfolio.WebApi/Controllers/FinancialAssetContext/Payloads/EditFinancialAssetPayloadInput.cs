namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Payloads;

public readonly struct EditFinancialAssetPayloadInput
{
    public EditFinancialAssetPayloadInput(string symbol, string? description, DateTime expirationDate, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
    {
        Symbol = symbol;
        Description = description;
        ExpirationDate = expirationDate;
        Status = status;
        InterestRate = interestRate;
        UnitaryPrice = unitaryPrice;
        QuantityAvailable = quantityAvailable;
    }

    public string Symbol { get; init; }
    public string? Description { get; init; }
    public DateTime ExpirationDate { get; init; }
    public string Status { get; init; }
    public decimal InterestRate { get; init; }
    public decimal UnitaryPrice { get; init; }
    public decimal QuantityAvailable { get; init; }
}
