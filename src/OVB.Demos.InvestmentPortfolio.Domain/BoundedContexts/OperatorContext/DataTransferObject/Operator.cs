using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;

public sealed record Operator
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Document { get; set; }

    public Operator(Guid id, string code, string name, string email, string password, string document)
    {
        Id = id;
        Code = code;
        Name = name;
        Email = email;
        Password = password;
        Document = document;
    }

    public IList<FinancialAsset>? FinancialAssets { get; set; }
}
