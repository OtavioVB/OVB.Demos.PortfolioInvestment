using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects.Exceptions;

namespace OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

public readonly struct IdentityValueObject
{
    private Guid Id { get; }
    public MethodResult<INotification> MethodResult { get; }

    private IdentityValueObject(Guid id, MethodResult<INotification> methodResult)
    {
        Id = id;
        MethodResult = methodResult;
    }

    public static IdentityValueObject Factory(Guid? id = null)
    {
        if (id == null)
            return new IdentityValueObject(
                id: Guid.NewGuid(),
                methodResult: MethodResult<INotification>.FactorySuccess());

        return new IdentityValueObject(
            id: id.Value,
            methodResult: MethodResult<INotification>.FactorySuccess());
    }

    public Guid GetIdentity()
    {
        BusinessValueObjectException.ThrowExceptionMethodResultIsError(MethodResult);

        return Id;
    }

    public string GetIdentityAsString()
        => GetIdentity().ToString();

    public static implicit operator MethodResult<INotification>(IdentityValueObject obj)
        => obj.MethodResult;
    public static implicit operator IdentityValueObject(Guid obj)
        => Factory(obj);
    public static implicit operator string(IdentityValueObject obj)
        => obj.GetIdentityAsString();
    public static implicit operator Guid(IdentityValueObject obj)
        => obj.GetIdentity();
}
