using Microsoft.Extensions.DependencyInjection;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.ExtractContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.OrderContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.PortfolioContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Inputs;
using OVB.Demos.InvestmentPortfolio.Application.UseCases.OrderContext.CreateOrder.Outputs;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Application;

public static class DependencyInjection
{
    public static void ApplyApplicationDependenciesConfiguration(
        this IServiceCollection serviceCollection, 
        string jwtBearerIssuerSigningKey, 
        string passwordHashPrivateKey,
        string sendEmailApiKey)
    {
        #region Services Dependencies Configuration

        serviceCollection.AddScoped<IOperatorService, OperatorService>((serviceProvider) 
            => new OperatorService(
                jwtBearerIssuerSigningKey: jwtBearerIssuerSigningKey,
                passwordHashPrivateKey: passwordHashPrivateKey,
                extensionOperatorRepository: serviceProvider.GetRequiredService<IExtensionOperatorRepository>()));

        serviceCollection.AddScoped<IFinancialAssetService, FinancialAssetService>((serviceProvider)
            => new FinancialAssetService(
                extensionFinancialAssetRepository: serviceProvider.GetRequiredService<IExtensionFinancialAssetRepository>(),
                baseFinancialAssetRepository: serviceProvider.GetRequiredService<IBaseRepository<FinancialAsset>>(),
                unitOfWork: serviceProvider.GetRequiredService<IUnitOfWork>(),
                sendEmailApiKey: sendEmailApiKey));

        serviceCollection.AddScoped<ICustomerService, CustomerService>((serviceProvider) 
            => new CustomerService(
                jwtBearerIssuerSigningKey: jwtBearerIssuerSigningKey,
                passwordHashPrivateKey: passwordHashPrivateKey,
                extensionCustomerRepository: serviceProvider.GetRequiredService<IExtensionCustomerRepository>()));

        serviceCollection.AddScoped<IPortfolioService, PortfolioService>((serviceProvider)
            => new PortfolioService(
                basePortfolioRepository: serviceProvider.GetRequiredService<IBaseRepository<Portfolio>>(),
                extensionPortfolioRepository: serviceProvider.GetRequiredService<IExtensionPortfolioRepository>(),
                unitOfWork: serviceProvider.GetRequiredService<IUnitOfWork>()));

        serviceCollection.AddScoped<IExtractService, ExtractService>((serviceProvider) 
            => new ExtractService(
                extractBaseRepository: serviceProvider.GetRequiredService<IBaseRepository<Extract>>(),
                unitOfWork: serviceProvider.GetRequiredService<IUnitOfWork>(),
                extensionExtractRepository: serviceProvider.GetRequiredService<IExtensionExtractRepository>()));

        serviceCollection.AddScoped<IOrderService, OrderService>((serviceProvider)
            => new OrderService(
                orderBaseRepository: serviceProvider.GetRequiredService<IBaseRepository<Order>>(),
                extensionFinancialAssetRepository: serviceProvider.GetRequiredService<IExtensionFinancialAssetRepository>(),
                unitOfWork: serviceProvider.GetRequiredService<IUnitOfWork>()));

        #endregion

        #region Use Cases Dependencies Configuration

        serviceCollection.AddScoped<IUseCase<CreateOrderUseCaseInput, CreateOrderUseCaseOutput>, CreateOrderUseCase>();

        #endregion
    }
}
