using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class CodeValueObjectValidationTests
{
    [Theory]
    [InlineData("OPT08FJK4")]
    [InlineData("CUS03487DJ")]
    [InlineData("ORDEM327DJ")]
    public void Code_Value_Object_Should_Be_Valid(string code)
    {
        // Arrange

        // Act
        var codeValueObject = CodeValueObject.Factory(code);

        // Assert
        Assert.True(codeValueObject.MethodResult.IsSuccess);
        Assert.Equal(code, codeValueObject.GetCode());
        Assert.Equal(code, codeValueObject);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("012345678901234567890123456789012")]
    public void Code_Value_Object_Should_Be_Not_Valid(string code)
    {
        // Arrange

        // Act
        var codeValueObject = CodeValueObject.Factory(code);

        // Assert
        Assert.False(codeValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(() => codeValueObject.GetCode());
    }

    [Theory]
    [InlineData("  ")]
    public void Code_Value_Object_Should_Be_Notification_Error_Message_Equal_As_Expected(string code)
    {
        // Arrange
        const string EXPECTED_CODE = "CODE_MUST_BE_NOT_EMPTY_OR_WHITESPACE";
        const string EXPECTED_MESSAGE = "O código não pode ser vazio ou conter apenas espaços em branco.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var codeValueObject = CodeValueObject.Factory(code);

        // Assert
        Assert.Single(codeValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, codeValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, codeValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, codeValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("012345678901234567890123456789012")]
    public void Code_Value_Object_Should_Be_Notification_Error_Message_Equal_As_Expected_When_Code_Length_Is_Greater_Than_The_Maximum(string code)
    {
        // Arrange
        const int EXPECTED_MAX_LENGTH = 32;

        const string EXPECTED_CODE = "CODE_LENGTH_NEED_TO_BE_LESS_THAN_MAX_LENGTH";
        string EXPECTED_MESSAGE = $"O código deve conter até {EXPECTED_MAX_LENGTH} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var codeValueObject = CodeValueObject.Factory(code);

        // Assert
        Assert.Single(codeValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_MAX_LENGTH, CodeValueObject.MAX_LENGTH);
        Assert.Equal(EXPECTED_CODE, codeValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, codeValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, codeValueObject.MethodResult.Notifications[0].Type);
    }
}
