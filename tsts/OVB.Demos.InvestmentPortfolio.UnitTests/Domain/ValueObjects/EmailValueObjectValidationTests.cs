using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class EmailValueObjectValidationTests
{
    [Theory]
    [InlineData("a.b@gmail.com")]
    [InlineData("a@gmail.com")]
    [InlineData("a@d.a")]
    [InlineData("otavio.carmanini@gmail.com")]
    [InlineData("teste@portfoliio.com.br")]
    public void Email_Value_Object_Should_Be_Valid(string email)
    {
        // Arrange

        // Act
        var emailValueObject = EmailValueObject.Factory(email);

        // Assert
        Assert.True(emailValueObject.MethodResult.IsSuccess);
        Assert.Equal(email, emailValueObject.GetEmail());
    }

    [Theory]
    [InlineData("@gmail.com")]
    [InlineData("a@")]
    [InlineData("otavio.carmanini@")]
    [InlineData("testeportfoliio.com.br")]
    [InlineData("  ")]
    public void Email_Value_Object_Should_Be_Not_Valid(string email)
    {
        // Arrange

        // Act
        var emailValueObject = EmailValueObject.Factory(email);

        // Assert
        Assert.False(emailValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(emailValueObject.GetEmail);
    }

    [Theory]
    [InlineData("  ")]
    public void Email_Value_Object_Should_Be_Send_Notification_Error_As_Expected_When_The_Email_Is_Whitespace(string email)
    {
        // Arrange
        const string EXPECTED_CODE = "EMAIL_CANNOT_BE_EMPTY_OR_WHITESPACE";
        const string EXPECTED_MESSAGE = "O email não pode ser vazio ou conter apenas espaços em branco.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var emailValueObject = EmailValueObject.Factory(email);

        // Assert
        Assert.Equal(EXPECTED_CODE, emailValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, emailValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, emailValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("@gmail.com")]
    [InlineData("a@")]
    [InlineData("otavio.carmanini@")]
    [InlineData("testeportfoliio.com.br")]
    public void Email_Value_Object_Should_Be_Send_Notification_Error_As_Expected_When_The_Email_Is_Not_Valid(string email)
    {
        // Arrange
        const string EXPECTED_CODE = "EMAIL_MUST_BE_VALID";
        const string EXPECTED_MESSAGE = "O email deve ser válido.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var emailValueObject = EmailValueObject.Factory(email);

        // Assert
        Assert.Equal(EXPECTED_CODE, emailValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, emailValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, emailValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901" +
        "23456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901" +
        "234567890123456789012345678901234567890123456789")]
    public void Email_Value_Object_Should_Be_Send_Notification_Error_As_Expected_When_The_Email_Length_Is_Greather_Than_The_Maximum_Length(string email)
    {
        // Arrange
        const int EXPECTED_EMAIL_MAX_LENGTH = 255;

        const string EXPECTED_CODE = "EMAIL_LENGTH_CANNOT_BE_GREATHER_THEN_MAXIMUM_ALLOWED";
        string EXPECTED_MESSAGE = $"O email não pode conter mais que {EXPECTED_EMAIL_MAX_LENGTH} caracteres.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var emailValueObject = EmailValueObject.Factory(email);

        // Assert
        Assert.Equal(EXPECTED_EMAIL_MAX_LENGTH, EmailValueObject.MAX_LENGTH);
        Assert.Equal(EXPECTED_CODE, emailValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, emailValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, emailValueObject.MethodResult.Notifications[0].Type);
    }
}
