using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.PortfolioContext.Sendloads;

public readonly struct QueryPortfoliosSendloadOutput
{
    public int Page { get; }
    public int Offset { get; }
    public QueryPortfoliosSendloadOutputItem[] Items { get; }
    public INotification[] Notifications { get; }

    private QueryPortfoliosSendloadOutput(int page, int offset, QueryPortfoliosSendloadOutputItem[] items, INotification[] notifications)
    {
        Page = page;
        Offset = offset;
        Items = items;
        Notifications = notifications;
    }

    public static QueryPortfoliosSendloadOutput Factory(int page, int offset, QueryPortfoliosSendloadOutputItem[] items, INotification[] notifications)
        => new(page, offset, items, notifications);
}

public readonly struct QueryPortfoliosSendloadOutputItem
{
    public string PortfolioId { get; }
    public decimal TotalPrice { get; }
    public decimal Quantity { get; }
    public QueryPortfoliosSendloadOutputItemFinancialAsset FinancialAsset { get; }

    private QueryPortfoliosSendloadOutputItem(string portfolioId, decimal totalPrice, decimal quantity, QueryPortfoliosSendloadOutputItemFinancialAsset financialAsset)
    {
        PortfolioId = portfolioId;
        TotalPrice = totalPrice;
        Quantity = quantity;
        FinancialAsset = financialAsset;
    }

    public static QueryPortfoliosSendloadOutputItem Factory(string portfolioId, decimal totalPrice, decimal quantity, QueryPortfoliosSendloadOutputItemFinancialAsset financialAsset)
        => new(portfolioId, totalPrice, quantity, financialAsset);
}

public readonly struct QueryPortfoliosSendloadOutputItemFinancialAsset
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

    private QueryPortfoliosSendloadOutputItemFinancialAsset(string financialAssetId, string symbol, string? description, string expirationDate, string index, string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
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

    public static QueryPortfoliosSendloadOutputItemFinancialAsset Factory(string financialAssetId, string symbol, string? description, string expirationDate,
        string index, string type, string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
        => new(financialAssetId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable);
}
