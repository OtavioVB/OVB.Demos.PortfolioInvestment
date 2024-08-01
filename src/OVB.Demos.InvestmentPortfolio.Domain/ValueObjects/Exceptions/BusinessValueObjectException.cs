using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;


public sealed class BusinessValueObjectException : Exception
{
    public BusinessValueObjectException(string message) : base(message)
    {
    }

    public static void ThrowExceptionMethodResultIsError(MethodResult<INotification> methodResult)
    {
        const string METHOD_RESULT_IS_ERROR_EXCEPTION_MESSAGE = "Is not possible to execute process, because the currently method result object is error.";

        if (methodResult.IsError)
            throw new BusinessValueObjectException(METHOD_RESULT_IS_ERROR_EXCEPTION_MESSAGE);
    }

    public static T ThrowExceptionIfTheObjectCannotBeNull<T>(T? resource)
    {
        const string RESOURCE_CANNOT_BE_NULL_EXCEPTION_MESSAGE = "Is not possible to query resource, because currently the object is null.";

        if (resource == null)
            throw new BusinessValueObjectException(RESOURCE_CANNOT_BE_NULL_EXCEPTION_MESSAGE);

        return resource;
    }
}
