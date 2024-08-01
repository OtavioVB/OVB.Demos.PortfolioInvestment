namespace OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

public interface INotification
{
    public string Code { get; }
    public string Message { get; }
    public string Type { get; }
}
