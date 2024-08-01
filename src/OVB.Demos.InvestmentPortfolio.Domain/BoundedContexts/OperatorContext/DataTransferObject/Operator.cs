using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;

public sealed record Operator
{
    public IdentityValueObject Id { get; set; }
    public CodeValueObject Code { get; set; }
    public NameValueObject Name { get; set; }
    public EmailValueObject Email { get; set; }
    public string Password { get; set; }
    public DocumentValueObject Document { get; set; }

    public Operator(IdentityValueObject id, CodeValueObject code, NameValueObject name, EmailValueObject email, string password, DocumentValueObject document)
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
