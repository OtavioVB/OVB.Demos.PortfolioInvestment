﻿using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.Enumerators;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext;
using OVB.Demos.InvestmentPortfolio.UnitTests.Domain.OperatorContext;
using System;

namespace OVB.Demos.InvestmentPortfolio.UnitTests.Domain.FinancialAssetContext;

public sealed class FinancialAssertDataTransferObjectValidationTests
{
    public static FinancialAsset FINANCIAL_ASSET_EXAMPLE = new FinancialAsset(
        id: Guid.NewGuid(),
        symbol: "CDB",
        description: "Certificado de Depósito Bancário",
        expirationDate: DateTime.Parse("2100-07-31"),
        index: FinancialAssetIndex.CDI,
        type: FinancialAssetType.PRE_FIXADO,
        status: FinancialAssetStatus.ATIVO,
        interestRate: 2.75m,
        unitaryPrice: 50,
        quantityAvailable: 575)
    {
        Operator = OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE
    };

    [Theory]
    [InlineData("CDB", "Certificado de Depósito Bancário", "2100-07-31", FinancialAssetIndex.CDI, FinancialAssetStatus.ATIVO, FinancialAssetType.PRE_FIXADO,
        2.75, 50.00, 525.984)]
    public void Financial_Assert_Data_Transfer_Object_Should_Be_Equal_Expected(string symbol, string? description,
        string expirationDate, FinancialAssetIndex index, FinancialAssetStatus status, FinancialAssetType type,
        decimal interestRate, decimal unitaryPrice, decimal quantityAvailable)
    {
        // Arrange 
        var financialAssetId = Guid.NewGuid();
        var expirationDateConvert = DateTime.Parse(expirationDate);

        // Act
        var financialAsset = new FinancialAsset(
            id: financialAssetId,
            symbol: symbol,
            description: description,
            expirationDate: expirationDateConvert,
            index: index,
            type: type,
            status: status,
            interestRate: interestRate,
            unitaryPrice: unitaryPrice,
            quantityAvailable: quantityAvailable);
        financialAsset.Operator = OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE;

        // Assert
        Assert.Equal(financialAssetId, financialAsset.Id);
        Assert.Equal(symbol, financialAsset.Symbol);
        Assert.Equal(description, financialAsset.Description);
        Assert.Equal(expirationDateConvert, financialAsset.ExpirationDate);
        Assert.Equal(index, financialAsset.Index.GetIndex());
        Assert.Equal(status, financialAsset.Status.GetStatus());
        Assert.Equal(type, financialAsset.Type.GetAssetType());
        Assert.Equal(interestRate, financialAsset.InterestRate.GetInterestRate());
        Assert.Equal(unitaryPrice, financialAsset.UnitaryPrice.GetUnitaryPrice());
        Assert.Equal(quantityAvailable, financialAsset.QuantityAvailable.GetQuantityAvailable());
        Assert.Equal(financialAsset.Operator, OperatorDataTransferObjectValidationTests.OPERATOR_EXAMPLE);
    }
}
