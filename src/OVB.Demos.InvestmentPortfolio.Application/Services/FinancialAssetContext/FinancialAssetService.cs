using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;

public sealed class FinancialAssetService : IFinancialAssetService
{
    private readonly IExtensionFinancialAssetRepository _extensionFinancialAssetRepository;
    private readonly IBaseRepository<FinancialAsset> _baseFinancialAssetRepository;

    public FinancialAssetService(
        IExtensionFinancialAssetRepository extensionFinancialAssetRepository, 
        IBaseRepository<FinancialAsset> baseFinancialAssetRepository)
    {
        _extensionFinancialAssetRepository = extensionFinancialAssetRepository;
        _baseFinancialAssetRepository = baseFinancialAssetRepository;
    }

    public async Task<MethodResult<INotification, CreateFinancialAssetServiceOutput>> CreateFinancialAssetServiceAsync(
        CreateFinancialAssetServiceInput input, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, CreateFinancialAssetServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var alreadyExistsFinancialAsset = await _extensionFinancialAssetRepository.VerifyFinancialAssetExistsBySymbolAsync(
            symbol: input.Symbol,
            cancellationToken: cancellationToken);

        var financialAssetAlreadyExistsValidation = ValidateFinancialAssetAlreadyExistsOnDatabase(alreadyExistsFinancialAsset);

        if (financialAssetAlreadyExistsValidation.IsError)
            return MethodResult<INotification, CreateFinancialAssetServiceOutput>.FactoryError(
                notifications: financialAssetAlreadyExistsValidation.Notifications);

        var financialAssetId = IdentityValueObject.Factory();

        var financialAsset = new FinancialAsset(
            id: financialAssetId,
            symbol: input.Symbol,
            description: input.Description,
            expirationDate: input.ExpirationDate,
            index: input.Index,
            type: input.Type,
            status: input.Status,
            interestRate: input.InterestRate,
            unitaryPrice: input.UnitaryPrice,
            quantityAvailable: input.QuantityAvailable);
        financialAsset.OperatorId = input.OperatorId;

        await _baseFinancialAssetRepository.AddAsync(
            entity: financialAsset,
            cancellationToken: cancellationToken);

        const string FINANCIAL_ASSET_CREATED_SUCCESS_NOTIFICATION_CODE = "FINANCIAL_ASSET_CREATED_SUCCESS";
        const string FINANCIAL_ASSET_CREATED_SUCCESS_NOTIFICATION_MESSAGE = "O ativo financeiro enviado foi cadastrado com sucesso.";

        return MethodResult<INotification, CreateFinancialAssetServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: FINANCIAL_ASSET_CREATED_SUCCESS_NOTIFICATION_CODE,
                message: FINANCIAL_ASSET_CREATED_SUCCESS_NOTIFICATION_MESSAGE)],
            output: CreateFinancialAssetServiceOutput.Factory(
                financialAsset: financialAsset));
    }

    private static MethodResult<INotification> ValidateFinancialAssetAlreadyExistsOnDatabase(
        bool alreadyExists)
    {
        const string FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL_NOTIFICATION_CODE = "FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL";
        const string FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL_NOTIFICATION_MESSAGE = "O ativo financeiro enviado já existe para o símbolo associado enviado.";

        if (alreadyExists)
            return MethodResult<INotification>.FactoryError(
                notifications: [Notification.FactoryFailure(
                    code: FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL_NOTIFICATION_CODE,
                    message: FINANCIAL_ASSET_ALREADY_EXISTS_WITH_SYMBOL_NOTIFICATION_MESSAGE)]);

        return MethodResult<INotification>.FactorySuccess();
    }
}
