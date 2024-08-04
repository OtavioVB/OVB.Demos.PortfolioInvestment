using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.WebApi.Controllers.OrderContext.Sendloads;

public readonly struct CreateOrderSendloadOutput
{
    public string OrderId { get; }
    public string CreatedAt { get; }
    public string Type { get; }
    public string Status { get; }
    public decimal Quantity { get; }
    public decimal UnitaryPrice { get; }
    public decimal TotalPrice { get; }
    public CreateOrderSendloadOutputFinancialAsset FinancialAsset { get; }
    public INotification[] Notifications { get; }

    private CreateOrderSendloadOutput(string orderId, string createdAt, string type, string status, decimal quantity, decimal unitaryPrice, decimal totalPrice,
        CreateOrderSendloadOutputFinancialAsset financialAsset, INotification[] notifications)
    {
        OrderId = orderId;
        CreatedAt = createdAt;
        Type = type;
        Status = status;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
        TotalPrice = totalPrice;
        FinancialAsset = financialAsset;
        Notifications = notifications;
    }

    public static CreateOrderSendloadOutput Factory(string orderId, string createdAt, string type, string status, decimal quantity, decimal unitaryPrice, decimal totalPrice,
        CreateOrderSendloadOutputFinancialAsset financialAsset, INotification[] notifications)
        => new(orderId, createdAt, type, status, quantity, unitaryPrice, totalPrice, financialAsset, notifications);
}

public readonly struct CreateOrderSendloadOutputFinancialAsset
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

    private CreateOrderSendloadOutputFinancialAsset(string financialAssetId, string symbol, string? description, string expirationDate, string index, string type,
        string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
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

    public static CreateOrderSendloadOutputFinancialAsset Factory(string financialAssetId, string symbol, string? description, string expirationDate, string index, string type,
        string status, decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
        => new(financialAssetId, symbol, description, expirationDate, index, type, status, interestRate, unitaryPrice, quantityAvailable);
}
