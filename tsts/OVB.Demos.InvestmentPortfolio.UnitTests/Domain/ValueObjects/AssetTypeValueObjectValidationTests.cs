using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class AssetTypeValueObjectValidationTests
{
    [Theory]
    [InlineData("PRE_FIXED")]
    [InlineData("POS_FIXED")]
    public void Asset_Type_Value_Object_Should_Be_Valid(string type)
    {
        // Arrange
        var typeEnum = Enum.Parse<FinancialAssetType>(type);

        // Act
        var assetTypeValueObject = AssetTypeValueObject.Factory(
            type: type);

        // Assert
        Assert.True(assetTypeValueObject.MethodResult.IsSuccess);
        Assert.Equal(type, assetTypeValueObject.GetTypeAsString());
        Assert.Equal(typeEnum, assetTypeValueObject.GetAssetType());
    }

    [Theory]
    [InlineData("PRE_FIXADO")]
    [InlineData("POS_FIXADO")]
    [InlineData("pos_fixed")]
    [InlineData("pre_fixed")]
    public void Asset_Type_Value_Object_Should_Be_Not_Valid(string type)
    {
        // Arrange

        // Act
        var assetTypeValueObject = AssetTypeValueObject.Factory(
            type: type);

        // Assert
        Assert.False(assetTypeValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(() => assetTypeValueObject.GetAssetType());
        Assert.Throws<BusinessValueObjectException>(assetTypeValueObject.GetTypeAsString);
    }

    [Theory]
    [InlineData("prefixed")]
    [InlineData("posfixed")]
    public void Asset_Type_Value_Object_Should_Be_Send_Notifications_As_Expected_On_Error(string type)
    {
        // Arrange
        const string EXPECTED_CODE = "ASSET_TYPE_IS_NOT_DEFINED";
        const string EXPECTED_MESSAGE = "O tipo do ativo financeiro não é suportado pela API.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var assetTypeValueObject = AssetTypeValueObject.Factory(
            type: type);

        // Assert
        Assert.Single(assetTypeValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, assetTypeValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, assetTypeValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, assetTypeValueObject.MethodResult.Notifications[0].Type);
    }
}
