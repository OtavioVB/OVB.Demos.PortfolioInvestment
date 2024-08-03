using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;

public sealed record Customer
{
    public IdentityValueObject Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public CodeValueObject Code { get; set; }
    public NameValueObject Name { get; set; }
    public DocumentValueObject Document { get; set; }
    public EmailValueObject Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    public Customer(IdentityValueObject id, DateTime createdAt, CodeValueObject code, NameValueObject name, DocumentValueObject document, EmailValueObject email, string passwordHash, string salt)
    {
        Id = id;
        CreatedAt = createdAt;
        Code = code;
        Name = name;
        Document = document;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
    }

    public IList<Order>? Orders { get; set; } 
    public IList<Extract>? Extracts { get; set; }
    public IList<Portfolio>? Portfolios { get; set; }
}
