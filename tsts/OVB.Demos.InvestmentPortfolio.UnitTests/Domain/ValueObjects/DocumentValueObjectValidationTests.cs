using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public sealed class DocumentValueObjectValidationTests
{
    [Theory]
    [InlineData("73186757029")]
    [InlineData("19337170094")]
    [InlineData("67897749045")]
    [InlineData("16027744000130")]
    [InlineData("63760265000108")]
    [InlineData("05034091000108")]
    public void Document_Value_Object_Should_Be_Valid(string document)
    {
        // Arrange
        var EXPECTED_PERSON_CLASSIFICATION = document.Length == 14 ? PersonClassification.LEGAL_PERSON : PersonClassification.NATURAL_PERSON;


        // Act
        var documentValueObject = DocumentValueObject.Factory(document);

        // Assert
        Assert.True(documentValueObject.MethodResult.IsSuccess);
        Assert.Equal(document, documentValueObject.GetDocument());
        Assert.Equal(EXPECTED_PERSON_CLASSIFICATION, documentValueObject.GetPersonClassification());
    }

    [Theory]
    [InlineData("7318675 029")]
    [InlineData("1933717009")]
    [InlineData("6789774904X")]
    [InlineData("160277444444KK")]
    [InlineData("   60265000108")]
    [InlineData("05034091000 KD")]
    public void Document_Value_Object_Should_Be_Not_Valid(string document)
    {
        // Arrange

        // Act
        var documentValueObject = DocumentValueObject.Factory(document);

        // Assert
        Assert.False(documentValueObject.MethodResult.IsSuccess);
        Assert.Throws<BusinessValueObjectException>(documentValueObject.GetDocument);
        Assert.Throws<BusinessValueObjectException>(() => documentValueObject.GetPersonClassification());
    }

    [Theory]
    [InlineData("1933717009")]
    [InlineData("94824782460265000108")]
    public void Document_Value_Object_Should_Be_Send_Notification_Error_Message_As_Expected_When_The_Document_Length_Is_Not_Expected(string document)
    {
        // Arrange
        const int EXPECTED_CPF_DOCUMENT_LENGTH = 11;
        const int EXPECTED_CNPJ_DOCUMENT_LENGTH = 14;

        const string EXPECTED_CODE = "DOCUMENT_LENGTH_NEED_TO_BE_VALID";
        string EXPECTED_MESSAGE = $"O documento deve conter {EXPECTED_CPF_DOCUMENT_LENGTH} dígitos para Pessoa Física ou {EXPECTED_CNPJ_DOCUMENT_LENGTH} dígitos para Pessoa Jurídica.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var documentValueObject = DocumentValueObject.Factory(document);

        // Assert
        Assert.Single(documentValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CPF_DOCUMENT_LENGTH, DocumentValueObject.CPF_DOCUMENT_REQUIRED_LENGTH);
        Assert.Equal(EXPECTED_CNPJ_DOCUMENT_LENGTH, DocumentValueObject.CNPJ_DOCUMENT_REQUIRED_LENGTH);
        Assert.Equal(EXPECTED_CODE, documentValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, documentValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, documentValueObject.MethodResult.Notifications[0].Type);
    }

    [Theory]
    [InlineData("6789774904X")]
    [InlineData("160277444444KK")]
    public void Document_Value_Object_Should_Be_Send_Notification_Error_Message_As_Expected_When_The_Document_Has_Different_Characters(string document)
    {
        // Arrange
        const string EXPECTED_CODE = "DOCUMENT_MUST_BE_VALID";
        const string EXPECTED_MESSAGE = "O documento deve conter apenas dígitos.";
        const string EXPECTED_TYPE = "Failure";

        // Act
        var documentValueObject = DocumentValueObject.Factory(document);

        // Assert
        Assert.Single(documentValueObject.MethodResult.Notifications);
        Assert.Equal(EXPECTED_CODE, documentValueObject.MethodResult.Notifications[0].Code);
        Assert.Equal(EXPECTED_MESSAGE, documentValueObject.MethodResult.Notifications[0].Message);
        Assert.Equal(EXPECTED_TYPE, documentValueObject.MethodResult.Notifications[0].Type);
    }
}
