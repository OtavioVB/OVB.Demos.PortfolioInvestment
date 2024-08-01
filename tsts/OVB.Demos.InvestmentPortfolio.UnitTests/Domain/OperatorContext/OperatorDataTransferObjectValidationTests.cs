using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OperatorContext;

public sealed class OperatorDataTransferObjectValidationTests
{
    public static Operator OPERATOR_EXAMPLE = new Operator(
            id: Guid.NewGuid(),
            code: "OPT09FADLKJ",
            name: "Otávio Carmanini",
            email: "otaviovb.developer@gmail.com",
            passwordHash: PasswordValueObject.Factory("123456789**&"),
            salt: "82J2347KHA4K",
            document: "00000000000");

    [Theory]
    [InlineData("OPT34DJXNHA4", "Otávio Carmanini", "54627477805", "otaviovb.developer@gmail.com", "82J2347KHA4K", "123456789**&")]
    public void Operator_Data_Transfer_Object_Should_Be_Equal_Expected(string code, string name, string document, string email, string salt, string password)
    {
        // Arrange
        var uuidGenerated = Guid.NewGuid();

        // Act
        var portfolioOperator = new Operator(
            id: uuidGenerated,
            code: code,
            name: name,
            email: email,
            passwordHash: PasswordValueObject.Factory(password),
            salt: salt,
            document: document);

        // Assert
        Assert.Equal(uuidGenerated, portfolioOperator.Id);
        Assert.Equal(code, portfolioOperator.Code);
        Assert.Equal(name, portfolioOperator.Name);
        Assert.Equal(email, portfolioOperator.Email);
        Assert.Equal(document, portfolioOperator.Document);
    }
}
