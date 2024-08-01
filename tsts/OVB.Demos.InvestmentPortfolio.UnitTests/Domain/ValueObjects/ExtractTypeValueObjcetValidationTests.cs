using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class ExtractTypeValueObjcetValidationTests
{
    [Theory]
    [InlineData("BUY")]
    [InlineData("SELL")]
    public void Extract_Type_Value_Object_Should_Be_Valid(string extractType)
    {
        // Arrange
        var EXTRACT_TYPE_ENUMERATOR_EXPECTED = Enum.Parse<ExtractType>(extractType);

        // Act
        var extractTypeValueObject = ExtractTypeValueObject.Factory(extractType);

        // Assert
        Assert.True(extractTypeValueObject.MethodResult.IsSuccess);
        Assert.Equal(EXTRACT_TYPE_ENUMERATOR_EXPECTED, extractTypeValueObject.GetExtractType());
        Assert.Equal<ExtractType>(EXTRACT_TYPE_ENUMERATOR_EXPECTED, extractTypeValueObject);
        Assert.Equal(extractType, extractTypeValueObject.GetExtractTypeAsString());
    }

    [Theory]
    [InlineData("TESTE")]
    [InlineData("XKDU83247HFJUHH")]
    [InlineData("   ")]
    [InlineData("")]
    public void Extract_Type_Value_Object_Should_Be_Not_Valid(string extractType)
    {
        // Arrange

        // Act
        var extractTypeValueObject = ExtractTypeValueObject.Factory(extractType);

        // Assert
        Assert.False(extractTypeValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(extractTypeValueObject.GetExtractTypeAsString);
        Assert.Throws<BusinessValueObjectException>(() => extractTypeValueObject.GetExtractType());
    }

    [Theory]
    [InlineData("TESTE")]
    [InlineData("XKDU83247HFJUHH")]
    [InlineData("   ")]
    [InlineData("")]
    public void Extract_Type_Value_Object_Should_Be_Send_Notification_Error_As_Expected_When_The_Extract_Is_Not_Defined(string extractType)
    {
        // Arrange
        const string EXPECTED_CODE = "EXTRACT_TYPE_IS_NOT_DEFINED";
        const string EXPECTED_MESSAGE = "O tipo de extrato associado não é um suportado pelo enumerador da API.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var extractTypeValueObject = ExtractTypeValueObject.Factory(extractType);

        // Assert
        Assert.Equal(EXPECTED_CODE, extractTypeValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, extractTypeValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, extractTypeValueObject.MethodResult.Notifications[0].Type);
    }
}
