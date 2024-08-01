using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class AssetSymbolValueObjectValidationTests
{
    [Theory]
    [InlineData("NTN-F")]
    [InlineData("NTN-B")]
    [InlineData("LFT")]
    [InlineData("LCI/LCA")]
    public void Asset_Symbol_Value_Object_Should_Be_Valid(string symbol)
    {
        // Arrange

        // Act
        var symbolValueObject = AssetSymbolValueObject.Factory(symbol);

        // Assert
        Assert.True(symbolValueObject.MethodResult.IsSuccess);
        Assert.Equal(symbol, symbolValueObject.GetSymbol());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("012345678901234567890123456789012")]
    public void Asset_Symbol_Value_Object_Should_Be_Not_Valid(string symbol)
    {
        // Arrange

        // Act
        var symbolValueObject = AssetSymbolValueObject.Factory(symbol);

        // Assert
        Assert.False(symbolValueObject.MethodResult.IsSuccess);
        Assert.NotEmpty(symbolValueObject.MethodResult.Notifications);
        Assert.Throws<BusinessValueObjectException>(symbolValueObject.GetSymbol);
    }

    [Theory]
    [InlineData("  ")]
    public void Asset_Symbol_Value_Object_Should_Be_Message_Equal_As_Expected_When_Whitespace(string symbol)
    {
        // Arrange
        const string EXPECTED_CODE = "ASSET_SYMBOL_CAN_NOT_BE_EMPTY_OR_WHITE_SPACE";
        const string EXPECTED_MESSAGE = "O símbolo do ativo financeiro não pode ser vazio ou conter espaços em branco.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var symbolValueObject = AssetSymbolValueObject.Factory(symbol);

        // Assert
        Assert.False(symbolValueObject.MethodResult.IsSuccess);
        Assert.Single(symbolValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, symbolValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, symbolValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, symbolValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("012345678901234567890123456789012")]
    public void Asset_Symbol_Value_Object_Should_Be_Message_Equal_As_Expected_When_Length_Greater_Than_The_Maximum(string symbol)
    {
        // Arrange
        const int EXPECTED_LENGTH = 32;
        const string EXPECTED_CODE = "ASSET_SYMBOL_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM";
        string EXPECTED_MESSAGE = $"O símbolo do ativo financeiro não pode conter mais que {EXPECTED_LENGTH} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var symbolValueObject = AssetSymbolValueObject.Factory(symbol);

        // Assert
        Assert.False(symbolValueObject.MethodResult.IsSuccess);
        Assert.Single(symbolValueObject.MethodResult.Notifications);
        Assert.Equal(AssetSymbolValueObject.MAX_LENGTH, EXPECTED_LENGTH);
        Assert.Equal(EXPECTED_CODE, symbolValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, symbolValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, symbolValueObject.MethodResult.Notifications[0].Type);
    }
}
