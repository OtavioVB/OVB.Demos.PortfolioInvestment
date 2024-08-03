using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.FinancialAssetContext.Sendloads;

public readonly struct QueryFinancialAssetSendloadOutput
{
    public int Page { get; }
    public int Offset { get; }
    public QueryFinancialAssetSendloadOutputItem[] Items { get; }
    public INotification[] Notifications { get; }

    private QueryFinancialAssetSendloadOutput(int page, int offset, QueryFinancialAssetSendloadOutputItem[] items, INotification[] notifications)
    {
        Page = page;
        Offset = offset;
        Items = items;
        Notifications = notifications;
    }

    public static QueryFinancialAssetSendloadOutput Factory(int page, int offset, QueryFinancialAssetSendloadOutputItem[] items, INotification[] notifications)
        => new(page, offset, items, notifications);
}

public readonly struct QueryFinancialAssetSendloadOutputItem
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

    private QueryFinancialAssetSendloadOutputItem(string financialAssetId, string symbol, string? description, string expirationDate, string index,
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

    public static QueryFinancialAssetSendloadOutputItem Factory(string financialAssetId, string symbol, string? description, string expirationDate, string index,
        string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
        => new(financialAssetId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable);
}