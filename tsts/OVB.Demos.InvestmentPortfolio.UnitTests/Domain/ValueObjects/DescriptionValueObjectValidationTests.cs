using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class DescriptionValueObjectValidationTests
{
    [Theory]
    [InlineData("Exemplo de Descrição")]
    [InlineData("Description Teste")]
    [InlineData(null)]
    public void Description_Value_Object_Should_Be_Valid(string? description)
    {
        // Arrange

        // Act
        var descriptionValueObject = DescriptionValueObject.Factory(description);

        // Assert
        Assert.True(descriptionValueObject.MethodResult.IsSuccess);
        Assert.Equal(description, descriptionValueObject.GetDescription());
    }

    [Theory]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345")]
    public void Description_Value_Object_Should_Be_Not_Valid(string? description)
    {
        // Arrange

        // Act
        var descriptionValueObject = DescriptionValueObject.Factory(description);

        // Assert
        Assert.False(descriptionValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(descriptionValueObject.GetDescription);
    }

    [Theory]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345")]
    public void Description_Value_Object_Should_Be_Send_Notification_Error_Message_As_Expected_When_The_Length_Is_Greather_Than_The_Maximum_Allowed(string? description)
    {
        // Arrange
        const int EXPECTED_MAX_LENGTH = 64;

        const string EXPECTED_CODE = "DESCRIPTION_LENGTH_CANNOT_BE_GREATHER_THE_MAXIMUM_ALLOWED";
        string EXPECTED_MESSAGE = $"A descrição não pode conter mais que {EXPECTED_MAX_LENGTH} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var descriptionValueObject = DescriptionValueObject.Factory(description);

        // Assert
        Assert.Single(descriptionValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_MAX_LENGTH, DescriptionValueObject.MAX_LENGTH);
        Assert.Equal(EXPECTED_CODE, descriptionValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, descriptionValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, descriptionValueObject.MethodResult.Notifications[0].Type);
    }
}
