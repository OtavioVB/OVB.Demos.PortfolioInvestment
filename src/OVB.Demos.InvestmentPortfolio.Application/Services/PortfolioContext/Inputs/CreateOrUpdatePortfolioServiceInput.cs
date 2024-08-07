﻿using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Inputs;

public readonly struct CreateOrUpdatePortfolioServiceInput
{
    public IdentityValueObject CustomerId { get; }
    public IdentityValueObject FinancialAssetId { get; }
    public QuantityValueObject Quantity { get; }
    public UnitaryPriceValueObject UnitaryPrice { get; }

    private CreateOrUpdatePortfolioServiceInput(IdentityValueObject customerId, IdentityValueObject financialAssetId, QuantityValueObject quantity, UnitaryPriceValueObject unitaryPrice)
    {
        CustomerId = customerId;
        FinancialAssetId = financialAssetId;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
    }

    public static CreateOrUpdatePortfolioServiceInput Factory(IdentityValueObject customerId, IdentityValueObject financialAssetId, QuantityValueObject quantity,
        UnitaryPriceValueObject unitaryPrice)
        => new(customerId, financialAssetId, quantity, unitaryPrice);

    public MethodResult<INotification> GetInputValidationResult()
        => MethodResult<INotification>.Factory(CustomerId, FinancialAssetId, Quantity, UnitaryPrice);
}
