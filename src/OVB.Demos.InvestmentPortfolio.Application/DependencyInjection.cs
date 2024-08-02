using Microsoft.Extensions.DependencyInjection;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext;
using OVB.Demos.InvestmentPortfolio.Application.Services.OperatorContext.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;

namespace OVB.Demos.InvestmentPortfolio.Application;

public static class DependencyInjection
{
    public static void ApplyApplicationDependenciesConfiguration(
        this IServiceCollection serviceCollection, 
        string jwtBearerIssuerSigningKey, 
        string passwordHashPrivateKey)
    {
        #region Services Dependencies Configuration

        serviceCollection.AddScoped<IOperatorService, OperatorService>((serviceProvider) 
            => new OperatorService(
                jwtBearerIssuerSigningKey: jwtBearerIssuerSigningKey,
                passwordHashPrivateKey: passwordHashPrivateKey,
                extensionOperatorRepository: serviceProvider.GetRequiredService<IExtensionOperatorRepository>()));

        #endregion
    }
}
