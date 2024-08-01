using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects.Exceptions;

public sealed class BusinessValueObjectExceptionValidationTests
{
    [Fact]
    public void Business_Value_Object_Exception_Should_Be_Throw_Exception_When_Method_Result_Is_Not_Success()
    {
        // Arrange

        // Act
        var methodResult = MethodResult<INotification>.FactoryError(
            notifications: []);

        // Assert
        Assert.Throws<BusinessValueObjectException>(() => BusinessValueObjectException.ThrowExceptionMethodResultIsError(methodResult));
    }

    [Fact]
    public void Business_Value_Object_Exception_Should_Be_Throw_Exception_When_object_Is_Null()
    {
        // Arrange

        // Act
        string? data = null;

        // Assert
        Assert.Throws<BusinessValueObjectException>(() => BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(data));
    }
}
