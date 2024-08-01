using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;
using System.Globalization;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class NameValueObjectValidationTests
{
    [Theory]
    [InlineData("Otavio Carmanini")]
    [InlineData("BANCO DO BRASIL SA")]
    [InlineData("RAZAO SOCIAL DA EMPRESA LTDA")]
    [InlineData("Razao SociAl DA EmPresa Ltda")]
    public void Name_Value_Object_Should_Be_Valid(string name)
    {
        // Arrange
        const string EXPECTED_CULTURE_INFO = "pt-br";
        var EXPECTED_NAME = CultureInfo.GetCultureInfo(EXPECTED_CULTURE_INFO).TextInfo.ToTitleCase(name);

        // Act
        var nameValueObject = NameValueObject.Factory(
            name: name);

        // Assert
        Assert.True(nameValueObject.MethodResult.IsSuccess);
        Assert.Equal(EXPECTED_NAME, nameValueObject.GetName());
        Assert.Equal(EXPECTED_NAME, nameValueObject);
    }

    [Theory]
    [InlineData("   ")]
    [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789")]
    public void Name_Value_Object_Should_Be_Not_Valid(string name)
    {
        // Arrange

        // Act
        var nameValueObject = NameValueObject.Factory(name);

        // Assert
        Assert.False(nameValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(nameValueObject.GetName);
    }

    [Theory]
    [InlineData("   ")]
    public void Name_Value_Object_Should_Send_Notification_Errors_As_Expected_When_The_Name_Is_WhiteSpace(string name)
    {
        // Arrange
        const string EXPECTED_CODE = "NAME_CANNOT_BE_EMPTY_OR_WHITESPACE";
        const string EXPECTED_MESSAGE = "O nome legal não pode ser vazio ou conter apenas espaços em branco.";
        const string EXPECTED_TYPE = "Failure";


        // Act
        var nameValueObject = NameValueObject.Factory(name);

        // Assert
        Assert.Single(nameValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_TYPE, nameValueObject.MethodResult.Notifications[0].Type);
        Assert.Equal(EXPECTED_MESSAGE, nameValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_CODE, nameValueObject.MethodResult.Notifications[0].Code);
    }

    [Theory]
    [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789")]
    public void Name_Value_Object_Should_Send_Notification_Errors_As_Expected_When_The_Name_Length_Is_Greater_Than_The_Maximum_Allowed(string name)
    {
        // Arrange
        const int EXPECTED_MAX_LENGTH = 64;

        const string EXPECTED_CODE = "NAME_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED";
        string EXPECTED_MESSAGE = $"O nome legal deve conter até {EXPECTED_MAX_LENGTH} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var nameValueObject = NameValueObject.Factory(name);

        // Assert
        Assert.Single(nameValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_MAX_LENGTH, NameValueObject.MAX_LENGTH);
        Assert.Equal(EXPECTED_TYPE, nameValueObject.MethodResult.Notifications[0].Type);
        Assert.Equal(EXPECTED_MESSAGE, nameValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_CODE, nameValueObject.MethodResult.Notifications[0].Code);
    }
}
