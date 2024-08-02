using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Sendloads;

public readonly struct CreateFinancialAssetSendloadOutput
{
    public string FinancialAssetId { get; }
    public string Symbol { get; }
    public string? Description { get; }
    public string ExpirationDate { get; }
    public string Index { get; }
    public string Type { get; }
    public string Status { get; }
    public decimal InterestRate { get; }
    public decimal UnitaryPrice { get; }
    public decimal QuantityAvailable { get; }

    private CreateFinancialAssetSendloadOutput(string financialAssetId, string symbol, string? description, string expirationDate, string index,
        string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
    {
        FinancialAssetId = financialAssetId;
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

    public static CreateFinancialAssetSendloadOutput Factory(string financialId, string symbol, string? description, string expirationDate, string index,
        string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
        => new(financialId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable);
}
