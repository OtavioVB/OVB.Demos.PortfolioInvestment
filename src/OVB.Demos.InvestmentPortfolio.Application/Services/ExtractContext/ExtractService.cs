using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.MethodResultContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext;
using OVB.Demos.InvestmentPortfolio.Domain.Utils.NotificationContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext;

public sealed class ExtractService : IExtractService
{
    private readonly IBaseRepository<Extract> _extractBaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExtractService(
        IBaseRepository<Extract> extractBaseRepository,
        IUnitOfWork unitOfWork)
    {
        _extractBaseRepository = extractBaseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MethodResult<INotification, CreateExtractServiceOutput>> CreateExtractServiceAsync(
        CreateExtractServiceInput input, CancellationToken cancellationToken)
    {
        var inputValidationResult = input.GetInputValidationResult();

        if (inputValidationResult.IsError)
            return MethodResult<INotification, CreateExtractServiceOutput>.FactoryError(
                notifications: inputValidationResult.Notifications);

        var extractId = IdentityValueObject.Factory();
        var extract = new Extract(
            id: extractId,
            createdAt: input.CreatedAt,
            type: input.Type,
            totalPrice: input.TotalPrice,
            unitaryPrice: input.UnitaryPrice,
            quantity: input.Quantity);
        extract.CustomerId = input.CustomerId;
        extract.FinancialAssetId = input.FinancialAssetId;

        await _extractBaseRepository.AddAsync(extract, cancellationToken);

        await _unitOfWork.ApplyDataContextTransactionChangeAsync(cancellationToken);

        const string CREATE_EXTRACT_HAS_DONE_NOTIFICATION_CODE = "CREATE_EXTRACT_HAS_DONE";
        const string CREATE_EXTRACT_HAS_DONE_NOTIFICATION_MESSAGE = "A criação do item extratual foi realizada com sucesso.";

        return MethodResult<INotification, CreateExtractServiceOutput>.FactorySuccess(
            notifications: [Notification.FactorySuccess(
                code: CREATE_EXTRACT_HAS_DONE_NOTIFICATION_CODE,
                message: CREATE_EXTRACT_HAS_DONE_NOTIFICATION_MESSAGE)],
            output: CreateExtractServiceOutput.Factory(
                extract: extract));
    }
}
