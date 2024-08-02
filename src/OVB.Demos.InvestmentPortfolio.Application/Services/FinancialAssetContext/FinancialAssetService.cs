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
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;

public sealed class FinancialAssetService : IFinancialAssetService
{
    private readonly IExtensionFinancialAssetRepository _extensionFinancialAssetRepository;
    private readonly IBaseRepository<FinancialAsset> _baseFinancialAssetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FinancialAssetService(
        IExtensionFinancialAssetRepository extensionFinancialAssetRepository, 
        IBaseRepository<FinancialAsset> baseFinancialAssetRepository,
        IUnitOfWork unitOfWork)
    {
        _extensionFinancialAssetRepository = extensionFinancialAssetRepository;
        _baseFinancialAssetRepository = baseFinancialAssetRepository;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.ApplyDataContextTransactionChangeAsync(cancellationToken);

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

    private static MethodResult<INotification> ValidateFinancialAssetNotFoundOnDatabase(
        FinancialAsset? financialAsset)
    {
        const string FINANCIAL_ASSET_NOT_FOUND_NOTIFICATION_CODE = "FINANCIAL_ASSET_NOT_FOUND";
        const string FINANCIAL_ASSET_NOT_FOUND_NOTIFICATION_MESSAGE = "O ativo financeiro enviado não foi possível ser encontrado.";

        if (financialAsset is null)
            return MethodResult<INotification>.FactoryError(
                notifications: [Notification.FactoryFailure(
                    code: FINANCIAL_ASSET_NOT_FOUND_NOTIFICATION_CODE,
                    message: FINANCIAL_ASSET_NOT_FOUND_NOTIFICATION_MESSAGE)]);

        return MethodResult<INotification>.FactorySuccess();
    }

    public async Task<MethodResult<INotification, UpdateFinancialAssetServiceOutput>> UpdateFinancialAssetServiceAsync(
        UpdateFinancialAssetServiceInput input,
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, UpdateFinancialAssetServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var queryFinancialAssetResult = await _baseFinancialAssetRepository.GetEntityByIdAsync(
            id: input.FinancialAssetId.GetIdentity(),
            cancellationToken: cancellationToken);

        var financialAssetNotFoundValidationResult = ValidateFinancialAssetNotFoundOnDatabase(queryFinancialAssetResult);

        if (financialAssetNotFoundValidationResult.IsError)
            return MethodResult<INotification, UpdateFinancialAssetServiceOutput>.FactoryError(
                notifications: financialAssetNotFoundValidationResult.Notifications);

        var financialAsset = queryFinancialAssetResult!;

        if (input.Symbol != null && input.Symbol != financialAsset.Symbol)
        {
            var alreadyExistsFinancialAssetBySymbol = await _extensionFinancialAssetRepository.VerifyFinancialAssetExistsBySymbolAsync(
                symbol: input.Symbol.Value,
                cancellationToken: cancellationToken);

            var financialAssetAlreadyExistsBySymbolValidation = ValidateFinancialAssetAlreadyExistsOnDatabase(alreadyExistsFinancialAssetBySymbol);

            if (financialAssetAlreadyExistsBySymbolValidation.IsError)
                return MethodResult<INotification, UpdateFinancialAssetServiceOutput>.FactoryError(
                    notifications: financialAssetAlreadyExistsBySymbolValidation.Notifications);
        }

        financialAsset.Symbol = input.Symbol is not null ? input.Symbol.Value : financialAsset.Symbol;
        financialAsset.Description = input.Description;
        financialAsset.ExpirationDate = input.ExpirationDate is not null ? input.ExpirationDate.Value : financialAsset.ExpirationDate;
        financialAsset.Status = input.Status is not null ? input.Status.Value : financialAsset.Status;
        financialAsset.InterestRate = input.InterestRate is not null ? input.InterestRate.Value : financialAsset.InterestRate;
        financialAsset.UnitaryPrice = input.UnitaryPrice is not null ? input.UnitaryPrice.Value : financialAsset.UnitaryPrice;
        financialAsset.QuantityAvailable = input.QuantityAvailable is not null ? input.QuantityAvailable.Value : financialAsset.QuantityAvailable;
        financialAsset.OperatorId = input.OperatorId;

        await _baseFinancialAssetRepository.UpdateAsync(
            entity: financialAsset,
            cancellationToken: cancellationToken);

        await _unitOfWork.ApplyDataContextTransactionChangeAsync(cancellationToken);

        const string FINANCIAL_ASSET_SUCCESS_NOTIFICATION_CODE = "UPDATE_FINANCIAL_ASSET_SUCCESS";
        string FINANCIAL_ASSET_SUCCESS_NOTIFICATION_MESSAGE = string.Format("O ativo financeiro {0} foi atualizado com sucesso.", financialAsset.Symbol);

        return MethodResult<INotification, UpdateFinancialAssetServiceOutput>.FactorySuccess(
            output: UpdateFinancialAssetServiceOutput.Factory(
                financialAsset: financialAsset),
            notifications: [Notification.FactorySuccess(
                code: FINANCIAL_ASSET_SUCCESS_NOTIFICATION_CODE,
                message: FINANCIAL_ASSET_SUCCESS_NOTIFICATION_MESSAGE)]);
    }
}
