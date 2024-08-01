using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed record AssetIndexValueObjectValidationTests
{
    [Theory]
    [InlineData("SELIC")]
    [InlineData("CDI")]
    [InlineData("IPCA")]
    public void Asset_Index_Value_Object_Should_Be_Valid(string index)
    {
        // Arrange
        var typeIndex = Enum.Parse<FinancialAssetIndex>(index);

        // Act
        var assetIndexValueObject = AssetIndexValueObject.Factory(
            index: index);

        // Assert
        Assert.True(assetIndexValueObject.MethodResult.IsSuccess);
        Assert.Equal(index, assetIndexValueObject.GetIndexAsString());
        Assert.Equal(typeIndex, assetIndexValueObject.GetIndex());
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("TESTE")]
    [InlineData("ERROR")]
    public void Asset_Index_Value_Object_Should_Be_Not_Valid(string index)
    {
        // Arrange

        // Act
        var assetIndexValueObject = AssetIndexValueObject.Factory(
            index: index);

        // Assert
        Assert.False(assetIndexValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(() => assetIndexValueObject.GetIndexAsString());
        Assert.Throws<BusinessValueObjectException>(() => assetIndexValueObject.GetIndex());
    }

    [Theory]
    [InlineData("ERROR_INDEX")]
    [InlineData("INVALID_TYPE")]
    public void Asset_Index_Value_Object_Notification_Message_Expected_On_Error(string index)
    {
        // Arrange
        const string EXPECTED_CODE = "ASSET_INDEX_IS_NOT_DEFINED";
        const string EXPECTED_MESSAGE = "O índice do ativo financeiro não é suportado pela API.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var assetIndexValueObject = AssetIndexValueObject.Factory(
            index: index);

        // Assert
        Assert.Single(assetIndexValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, assetIndexValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, assetIndexValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, assetIndexValueObject.MethodResult.Notifications[0].Type);
    }
}
