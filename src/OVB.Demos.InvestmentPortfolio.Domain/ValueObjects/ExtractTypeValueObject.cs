using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct ExtractTypeValueObject
{
    public MethodResult<INotification> MethodResult { get; }
    private ExtractType? ExtractType { get; }

    private ExtractTypeValueObject(MethodResult<INotification> methodResult, ExtractType? extractType = null)
    {
        MethodResult = methodResult;
        ExtractType = extractType;
    }

    private const string EXTRACT_TYPE_IS_NOT_DEFINED_NOTIFICATION_CODE = "EXTRACT_TYPE_IS_NOT_DEFINED";
    private const string EXTRACT_TYPE_IS_NOT_DEFINED_NOTIFICATION_MESSAGE = "O tipo de extrato associado não é um suportado pelo enumerador da API.";

    public static ExtractTypeValueObject Factory(string extract)
    {
        const int CAPACITY_NOTIFICATIONS_POSSIBLE = 2;

        var notifications = new List<INotification>(CAPACITY_NOTIFICATIONS_POSSIBLE);

        var isPossibleToConvertAsExtractTypeEnumerator = Enum.TryParse<ExtractType>(
            value: extract,
            ignoreCase: false,
            result: out var typeExtract);

        if (!isPossibleToConvertAsExtractTypeEnumerator)
            return new ExtractTypeValueObject(
                methodResult: MethodResult<INotification>.FactoryError(
                    notifications: [Notification.FactoryFailure(
                        code: EXTRACT_TYPE_IS_NOT_DEFINED_NOTIFICATION_CODE,
                        message: EXTRACT_TYPE_IS_NOT_DEFINED_NOTIFICATION_MESSAGE)]));

        return new ExtractTypeValueObject(
            methodResult: MethodResult<INotification>.FactorySuccess(),
            extractType: typeExtract);
    }

    public ExtractType GetExtractType()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return BusinessValueObjectException.ThrowExceptionIfTheObjectCannotBeNull(ExtractType)!.Value;
    }

    public string GetExtractTypeAsString()
        => GetExtractType().ToString();

    public static implicit operator ExtractType(ExtractTypeValueObject obj)
        => obj.GetExtractType();
    public static implicit operator string(ExtractTypeValueObject obj)
        => obj.GetExtractTypeAsString();
    public static implicit operator MethodResult<INotification>(ExtractTypeValueObject obj)
        => obj.MethodResult;
    public static implicit operator ExtractTypeValueObject(string obj)
        => Factory(obj);
    public static implicit operator ExtractTypeValueObject(ExtractType obj)
        => Factory(obj.ToString());
}
