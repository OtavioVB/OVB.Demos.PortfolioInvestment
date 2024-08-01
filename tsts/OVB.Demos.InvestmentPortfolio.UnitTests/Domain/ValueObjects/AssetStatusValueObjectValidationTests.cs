using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class AssetStatusValueObjectValidationTests
{
    [Theory]
    [InlineData("ACTIVE")]
    [InlineData("INACTIVE")]
    public void Asset_Status_Value_Object_Should_Be_Valid(string status)
    {
        // Arrange
        var statusEnum = Enum.Parse<FinancialAssetStatus>(status);

        // Act
        var statusValueObject = AssetStatusValueObject.Factory(
            type: status);

        // Assert
        Assert.True(statusValueObject.MethodResult.IsSuccess);
        Assert.Equal(statusEnum, statusValueObject.GetStatus());
        Assert.Equal(status, statusValueObject.GetStatusAsString());
    }

    [Theory]
    [InlineData("BLOCKED")]
    [InlineData("DECLINED")]
    [InlineData("PENDING")]
    public void Asset_Status_Value_Object_Should_Be_Not_Valid(string status)
    {
        // Arrange


        // Act
        var statusValueObject = AssetStatusValueObject.Factory(
            type: status);

        // Assert
        Assert.False(statusValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(() => statusValueObject.GetStatusAsString());
        Assert.Throws<BusinessValueObjectException>(() => statusValueObject.GetStatus());
    }

    [Theory]
    [InlineData("TESTE")]
    [InlineData("ERROR")]
    [InlineData("NCUHOK")]
    public void Asset_Status_Value_Object_Should_Be_Notification_Equal_A_Expected(string status)
    {
        // Arrange
        const string EXPECTED_CODE = "ASSET_STATUS_IS_NOT_DEFINED";
        const string EXPECTED_MESSAGE = "O status do ativo financeiro não é suportado pela API.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var statusValueObject = AssetStatusValueObject.Factory(
            type: status);

        // Assert
        Assert.False(statusValueObject.MethodResult.IsSuccess);
        Assert.Equal(EXPECTED_CODE, statusValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, statusValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, statusValueObject.MethodResult.Notifications[0].Type);
    }
}
