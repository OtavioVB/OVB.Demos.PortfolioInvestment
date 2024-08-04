using Microsoft.Extensions.DependencyInjection;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.CustomerContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.FinancialAssetContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
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

        #endregion
    }
}
