namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Models;

public readonly struct SendEmailModel
{
    public string from { get; }
    public string to { get; }
    public string subject { get; }
    public string html { get; }

    private SendEmailModel(string from, string to, string subject, string html)
    {
        this.from = from;
        this.to = to;
        this.subject = subject;
        this.html = html;
    }

    public static SendEmailModel Factory(string from, string to, string subject, string html)
        => new(from, to subject, html);
}
