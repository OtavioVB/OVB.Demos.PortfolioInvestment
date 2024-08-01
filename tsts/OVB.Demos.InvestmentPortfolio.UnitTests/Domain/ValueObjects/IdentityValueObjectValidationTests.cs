using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class IdentityValueObjectValidationTests
{
    [Fact]
    public void Identity_Value_Object_Should_Be_Valid()
    {
        // Arrange
        var uuidGenerated = Guid.NewGuid();
        var uuidGeneratedAsString = uuidGenerated.ToString();

        // Act
        var identityValueObject1 = IdentityValueObject.Factory(uuidGenerated);
        var identityValueObject2 = IdentityValueObject.Factory();

        // Assert
        Assert.True(identityValueObject1.MethodResult.IsSuccess);
        Assert.True(identityValueObject2.MethodResult.IsSuccess);
        Assert.Equal(uuidGenerated, identityValueObject1.GetIdentity());
        Assert.Equal(uuidGenerated, identityValueObject1);
        Assert.Equal(uuidGeneratedAsString, identityValueObject1.GetIdentityAsString());
        Assert.Equal(uuidGeneratedAsString, identityValueObject1);
    }
}
