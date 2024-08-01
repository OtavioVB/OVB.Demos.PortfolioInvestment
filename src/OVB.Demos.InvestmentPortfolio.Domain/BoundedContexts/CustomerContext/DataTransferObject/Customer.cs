using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;

public sealed record Customer
{
    public IdentityValueObject Id { get; set; }
    public CodeValueObject Code { get; set; }
    public NameValueObject Name { get; set; }
    public DocumentValueObject Document { get; set; }
    public EmailValueObject Email { get; set; }

    public Customer(IdentityValueObject id, CodeValueObject code, NameValueObject name, DocumentValueObject document, EmailValueObject email)
    {
        Id = id;
        Code = code;
        Name = name;
        Document = document;
        Email = email;
    }

    public IList<Order>? Orders { get; set; } 
    public IList<Extract>? Extracts { get; set; }
}
