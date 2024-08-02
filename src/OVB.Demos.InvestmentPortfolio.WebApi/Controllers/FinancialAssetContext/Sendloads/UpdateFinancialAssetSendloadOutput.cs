using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Sendloads;

public readonly struct UpdateFinancialAssetSendloadOutput
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
    public INotification[] Notifications { get; }

    private UpdateFinancialAssetSendloadOutput(string financialAssetId, string symbol, string? description, string expirationDate, string index,
        string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable, INotification[] notifications)
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
        Notifications = notifications;
    }

    public static UpdateFinancialAssetSendloadOutput Factory(string financialAssetId, string symbol, string? description, string expirationDate, string index,
        string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable, INotification[] notifications)
        => new(financialAssetId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable, notifications);
}
