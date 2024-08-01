using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct DocumentValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private string? Document { get; }
    private PersonClassification? Classification { get; }

    private DocumentValueObject(MethodResult<INotification> methodResult, string? document = null, PersonClassification? classification = null)
    {
        MethodResult = methodResult;
        Document = document;
        Classification = classification;
    }

    public const int CPF_DOCUMENT_REQUIRED_LENGTH = 11;
    public const int CNPJ_DOCUMENT_REQUIRED_LENGTH = 14;

    private const string DOCUMENT_LENGTH_NEED_TO_VALID_NOTIFICATION_CODE = "DOCUMENT_LENGTH_NEED_TO_BE_VALID";
    private static string DOCUMENT_LENGTH_NEED_TO_VALID_NOTIFICATION_MESSAGE => $"O documento deve conter {CPF_DOCUMENT_REQUIRED_LENGTH} dígitos para Pessoa Física ou {CNPJ_DOCUMENT_REQUIRED_LENGTH} dígitos para Pessoa Jurídica.";

    private const string DOCUMENT_MUST_BE_VALID_NOTIFICATION_CODE = "DOCUMENT_MUST_BE_VALID";
    private const string DOCUMENT_MUST_BE_VALID_NOTIFICATION_MESSAGE = "O documento deve conter apenas dígitos.";

    public static DocumentValueObject Factory(string document)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        if (document.Length != CPF_DOCUMENT_REQUIRED_LENGTH && document.Length != CNPJ_DOCUMENT_REQUIRED_LENGTH)
            notifications.Add(Notification.FactoryFailure(
                code: DOCUMENT_LENGTH_NEED_TO_VALID_NOTIFICATION_CODE,
                message: DOCUMENT_LENGTH_NEED_TO_VALID_NOTIFICATION_MESSAGE));

        foreach (var character in document)
            if (!char.IsDigit(character))
            {
                notifications.Add(Notification.FactoryFailure(
                code: DOCUMENT_MUST_BE_VALID_NOTIFICATION_CODE,
                message: DOCUMENT_MUST_BE_VALID_NOTIFICATION_MESSAGE));
                break;
            }

        if (Notification.HasAnyNotifications(notifications))
            return new DocumentValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: notifications.ToArray()));

        return new DocumentValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            document: document,
            classification: GetPersonClassificationAccordingToDocument(document));
    }

    private static PersonClassification GetPersonClassificationAccordingToDocument(string document)
    {
        if (document.Length == CNPJ_DOCUMENT_REQUIRED_LENGTH)
            return PersonClassification.LEGAL_PERSON;

        if (document.Length == CPF_DOCUMENT_REQUIRED_LENGTH)
            return PersonClassification.NATURAL_PERSON;

        throw new ArgumentOutOfRangeException(nameof(GetPersonClassificationAccordingToDocument));
    }

    public string GetDocument()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Document);
    }

    public PersonClassification GetPersonClassification()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(Classification)!.Value;
    }

    public static implicit operator string(DocumentValueObject obj)
        => obj.GetDocument();
    public static implicit operator PersonClassification(DocumentValueObject obj)
        => obj.GetPersonClassification();
    public static implicit operator DocumentValueObject(string obj)
        => Factory(obj);
    public static implicit operator MethodResult<INotification>(DocumentValueObject obj)
        => obj.MethodResult;
}
