using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Models;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;
using System.Net.Http;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;

public sealed class FinancialAssetService : IFinancialAssetService
{
    private readonly IExtensionFinancialAssetRepository _extensionFinancialAssetRepository;
    private readonly IBaseRepository<FinancialAsset> _baseFinancialAssetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _sendEmailApiKey;

    public FinancialAssetService(
        IExtensionFinancialAssetRepository extensionFinancialAssetRepository, 
        IBaseRepository<FinancialAsset> baseFinancialAssetRepository,
        IUnitOfWork unitOfWork,
        string sendEmailApiKey)
    {
        _extensionFinancialAssetRepository = extensionFinancialAssetRepository;
        _baseFinancialAssetRepository = baseFinancialAssetRepository;
        _unitOfWork = unitOfWork;
        _sendEmailApiKey = sendEmailApiKey; 
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
        string FINANCIAL_ASSET_SUCCESS_NOTIFICATION_MESSAGE = string.Format("O ativo financeiro {0} foi atualizado com sucesso.", financialAsset.Symbol.GetSymbol());

        return MethodResult<INotification, UpdateFinancialAssetServiceOutput>.FactorySuccess(
            output: UpdateFinancialAssetServiceOutput.Factory(
                financialAsset: financialAsset),
            notifications: [Notification.FactorySuccess(
                code: FINANCIAL_ASSET_SUCCESS_NOTIFICATION_CODE,
                message: FINANCIAL_ASSET_SUCCESS_NOTIFICATION_MESSAGE)]);
    }

    public async Task<MethodResult<INotification, QueryByIdFinancialAssetServiceOutput>> QueryByIdFinancialAssetServiceAsync(
        QueryByIdFinancialAssetServiceInput input,
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, QueryByIdFinancialAssetServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var queryFinancialAssetByIdResult = await _baseFinancialAssetRepository.GetEntityByIdAsync(
            id: input.FinancialAssetId,
            cancellationToken: cancellationToken);

        var financialAssetNotFoundValidationResult = ValidateFinancialAssetNotFoundOnDatabase(queryFinancialAssetByIdResult);

        if (financialAssetNotFoundValidationResult.IsError)
            return MethodResult<INotification, QueryByIdFinancialAssetServiceOutput>.FactoryError(
                notifications: financialAssetNotFoundValidationResult.Notifications);

        const string QUERY_BY_ID_FINANCIAL_ASSET_HAS_DONE_NOTIFICATION_CODE = "QUERY_BY_ID_FINANCIAL_ASSET_HAS_DONE";
        const string QUERY_BY_ID_FINANCIAL_ASSET_HAS_DONE_NOTIFICATION_MESSAGE = "A consulta do ativo financeiro pela sua identificação foi realizada com sucesso.";

        return MethodResult<INotification, QueryByIdFinancialAssetServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: QUERY_BY_ID_FINANCIAL_ASSET_HAS_DONE_NOTIFICATION_CODE,
                message: QUERY_BY_ID_FINANCIAL_ASSET_HAS_DONE_NOTIFICATION_MESSAGE)],
            output: QueryByIdFinancialAssetServiceOutput.Factory(
                financialAsset: queryFinancialAssetByIdResult!));
    }

    public async Task<MethodResult<INotification, QueryFinancialAssetServiceOutput>> QueryFinancialAssetServiceAsync(
        QueryFinancialAssetServiceInput input, 
        CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, QueryFinancialAssetServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var queryFinancialAssetsResult = await _extensionFinancialAssetRepository.QueryFinancialAssetAsNoTrackingByPaginationAsync(
            page: input.Page,
            offset: input.Offset,
            cancellationToken: cancellationToken);

        const string QUERY_FINANCIAL_ASSETS_SUCCESS_NOTIFICATION_CODE = "QUERY_FINANCIAL_ASSETS_SUCCESS";
        const string QUERY_FINANCIAL_ASSETS_SUCCESS_NOTIFICATION_MESSAGE = "A consulta dos ativos financeiros foi realizada com sucesso.";

        return MethodResult<INotification, QueryFinancialAssetServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: QUERY_FINANCIAL_ASSETS_SUCCESS_NOTIFICATION_CODE,
                message: QUERY_FINANCIAL_ASSETS_SUCCESS_NOTIFICATION_MESSAGE)],
            output: QueryFinancialAssetServiceOutput.Factory(
                page: input.Page,
                offset: input.Offset,
                financialAssets: queryFinancialAssetsResult));
    }

    public async Task<MethodResult<INotification>> AdviceFinancialAssetUpcomingExpirationDateAsync(
        CancellationToken cancellationToken)
    {
        const int ADVICE_REQUIRED_EXPIRATION_UPCOMING_DAYS = 15;

        var queryFinancialAssetsUpcomingExpirationDateResult = await _extensionFinancialAssetRepository.QueryFinancialAssetAsNoTrackingWhenExpirationDateIsLessThanExpectedDateIncludingOperatorsAsync(
            expirationDateExpected: DateTime.UtcNow.Date.AddDays(ADVICE_REQUIRED_EXPIRATION_UPCOMING_DAYS),
            cancellationToken: cancellationToken);

        const string FINANCIAL_ASSET_ADVICE_NOT_FOUND_NOTIFICATION_CODE = "FINANCIAL_ASSET_ADVICE_NOT_FOUND";
        const string FINANCIAL_ASSET_ADVICE_NOT_FOUND_NOTIFICATION_MESSAGE = "Nenhum aviso de vencimento próximo de algum ativo financeiro foi encontrado para ser enviado para os operadores gestores.";

        if (queryFinancialAssetsUpcomingExpirationDateResult.Length == 0)
            return MethodResult<INotification>.FactorySuccess(
                notifications: [Notification.FactoryInformation(
                    code: FINANCIAL_ASSET_ADVICE_NOT_FOUND_NOTIFICATION_CODE,
                    message: FINANCIAL_ASSET_ADVICE_NOT_FOUND_NOTIFICATION_MESSAGE)]);

        foreach (var financialAsset in queryFinancialAssetsUpcomingExpirationDateResult)
        {
            _ = SendEmailAboutAdviceExpirationFinancialAsset(
                destinationEmail: financialAsset.Operator!.Email,
                symbol: financialAsset.Symbol,
                financialAssetId: financialAsset.Id,
                operatorName: financialAsset.Operator.Name,
                expirationDate: financialAsset.ExpirationDate);
        }

        const string FINANCIAL_ASSET_ADVICE_HAS_SENT_SUCCESS_NOTIFICATION_CODE = "FINANCIAL_ASSET_ADVICE_HAS_SENT_SUCCESS";
        const string FINANCIAL_ASSET_ADVICE_HAS_SENT_SUCCESS_NOTIFICATION_MESSAGE = "Todos os avisos de vencimento próximo de ativos financeiros foram enviados para os gestores.";

        return MethodResult<INotification>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: FINANCIAL_ASSET_ADVICE_HAS_SENT_SUCCESS_NOTIFICATION_CODE,
                message: FINANCIAL_ASSET_ADVICE_HAS_SENT_SUCCESS_NOTIFICATION_MESSAGE)]);
    }

    private async Task SendEmailAboutAdviceExpirationFinancialAsset(
        EmailValueObject destinationEmail,
        AssetSymbolValueObject symbol,
        IdentityValueObject financialAssetId,
        NameValueObject operatorName,
        AssetExpirationDateValueObject expirationDate)
    {
        const string FROM_EMAIL = "onboarding@resend.dev";
        const string ENDPOINT = "https://api.resend.com/emails";
        const string DESTINATION_EMAIL_ALLOWED = "contato.otaviovbsc@gmail.com";

        const string REQUIRED_HEADER_AUTHORIZATION_NAME = "Authorization";

        using var httpClient = HttpClientFactory.Create();

        httpClient.DefaultRequestHeaders.Add(
            name: REQUIRED_HEADER_AUTHORIZATION_NAME,
            value: _sendEmailApiKey);

        string SUBJECT = string.Format("Aviso de Expiração do Ativo Financeiro {0}", symbol.GetSymbol());
        string HTML = string.Format("Prezado {0} inscrito como operador gestor na plataforma sobre o email {1}, o ativo financeiro '{2}' identificado por {3} expirará logo mais. Esteja atento ao prazo de expiração, que acontecerá '{4}'. Atenciosamente, Portfólio de Investimentos", 
            operatorName.GetName(), 
            destinationEmail.GetEmail(),
            symbol.GetSymbol(),
            financialAssetId.GetIdentityAsString(),
            expirationDate.GetExpirationDateAsString());

        var email = SendEmailModel.Factory(
            from: FROM_EMAIL,
            to: DESTINATION_EMAIL_ALLOWED,
            subject: SUBJECT,
            html: HTML);

        await httpClient.PostAsJsonAsync(
            requestUri: ENDPOINT,
            value: email);
    }
}
