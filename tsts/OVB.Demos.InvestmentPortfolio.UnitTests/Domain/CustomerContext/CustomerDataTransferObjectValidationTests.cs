using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;

public sealed class CustomerDataTransferObjectValidationTests
{
    public const string EXAMPLE_PWD_TEST = "_836hjfk7DH8!";

    public static Customer CUSTOMER_EXAMPLE = new Customer(
        id: Guid.NewGuid(),
        createdAt: DateTime.UtcNow,
        code: "CS084HFSJ",
        name: "Otávio Carmnini",
        document: "36478348075",
        email: "otaviovb.developer@gmail.com",
        passwordHash: "8b440d3cfef73bda67eb80626dcb359b0ddd1cd319656c3eadc5bd452414d676",
        salt: "82J2347KHA4K");

    [Theory]
    [InlineData("CS084HFSJ", "Otávio Carmnini", "36478348075", "otaviovb.developer@gmail.com", "82J2347KHA4K", "123456789**&")]
    public void Customer_Data_Transfer_Object_Should_Be_Equal_The_Params_Send_To_Constructor(string code, string name, string document, string email, string salt, string password)
    {
        // Arrange
        var uuidGenerated = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;

        // Act
        var customer = new Customer(
            id: uuidGenerated,
            createdAt: createdAt,
            code: code,
            name: name,
            document: document, 
            email: email,
            salt: salt,
            passwordHash: password);

        // Assert
        Assert.Equal<Guid>(uuidGenerated, customer.Id);
        Assert.Equal(code, customer.Code);
        Assert.Equal(name, customer.Name);
        Assert.Equal(document, customer.Document);
        Assert.Equal(email, customer.Email);
        Assert.Equal(createdAt, customer.CreatedAt);
        Assert.Equal(password, customer.PasswordHash);
        Assert.Equal(salt, customer.Salt);
    }
}
