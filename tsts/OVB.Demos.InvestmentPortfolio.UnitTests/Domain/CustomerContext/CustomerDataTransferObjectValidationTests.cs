using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.CustomerContext;

public sealed class CustomerDataTransferObjectValidationTests
{
    public static Customer CUSTOMER_EXAMPLE = new Customer(
        id: Guid.NewGuid(),
        code: "CS084HFSJ",
        name: "Otávio Carmnini",
        document: "36478348075",
        email: "otaviovb.developer@gmail.com");

    [Theory]
    [InlineData("CS084HFSJ", "Otávio Carmnini", "36478348075", "otaviovb.developer@gmail.com")]
    public void Customer_Data_Transfer_Object_Should_Be_Equal_The_Params_Send_To_Constructor(string code, string name, string document, string email)
    {
        // Arrange
        var uuidGenerated = Guid.NewGuid();

        // Act
        var customer = new Customer(
            id: uuidGenerated,
            code: code,
            name: name,
            document: document, 
            email: email);

        // Assert
        Assert.Equal(uuidGenerated, customer.Id);
        Assert.Equal(code, customer.Code);
        Assert.Equal(name, customer.Name);
        Assert.Equal(document, customer.Document);
        Assert.Equal(email, customer.Email);
    }
}
