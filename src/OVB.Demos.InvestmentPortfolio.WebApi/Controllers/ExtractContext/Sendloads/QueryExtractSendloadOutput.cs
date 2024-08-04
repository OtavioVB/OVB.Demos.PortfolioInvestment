using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.ExtractContext.Sendloads;

public readonly struct QueryExtractSendloadOutput
{
    public int Page { get; }
    public int Offset { get; }
    public QueryExtractSendloadOutputItem[] Items { get; }
    public INotification[] Notifications { get; }

    private QueryExtractSendloadOutput(int page, int offset, QueryExtractSendloadOutputItem[] items, INotification[] notifications)
    {
        Page = page;
        Offset = offset;
        Items = items;
        Notifications = notifications;
    }

    public static QueryExtractSendloadOutput Factory(int page, int offset, QueryExtractSendloadOutputItem[] items, INotification[] notifications)
        => new(page, offset, items, notifications);
}

public readonly struct QueryExtractSendloadOutputItem
{
    public string ExtractId { get; }
    public DateTime CreatedAt { get; }
    public string Type { get; }
    public decimal TotalPrice { get; }
    public decimal UnitaryPrice { get; }
    public decimal Quantity { get; }
    public QueryExtractSendloadOutputItemFinancialAsset FinancialAsset { get; }

    private QueryExtractSendloadOutputItem(string extractId, DateTime createdAt, string type, decimal totalPrice, decimal unitaryPrice, decimal quantity, QueryExtractSendloadOutputItemFinancialAsset financialAsset)
    {
        ExtractId = extractId;
        CreatedAt = createdAt;
        Type = type;
        TotalPrice = totalPrice;
        UnitaryPrice = unitaryPrice;
        Quantity = quantity;
        FinancialAsset = financialAsset;
    }

    public static QueryExtractSendloadOutputItem Factory(string extractId, DateTime createdAt, string type, decimal totalPrice, decimal unitaryPrice, decimal quantity,
        QueryExtractSendloadOutputItemFinancialAsset financialAsset)
        => new(extractId, createdAt, type, totalPrice, unitaryPrice, quantity, financialAsset);
}

public readonly struct QueryExtractSendloadOutputItemFinancialAsset
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

    private QueryExtractSendloadOutputItemFinancialAsset(string financialAssetId, string symbol, string? description, string expirationDate, string index,
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

    public static QueryExtractSendloadOutputItemFinancialAsset Factory(string financialAssetId, string symbol, string? description, string expirationDate, string index,
        string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
        => new(financialAssetId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable);
}