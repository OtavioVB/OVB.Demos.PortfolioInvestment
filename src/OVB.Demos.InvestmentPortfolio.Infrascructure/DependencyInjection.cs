using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Base.Interfaces;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Repositories.Extensions;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.UnitOfWork.Interfaces;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure;

public static class DependencyInjection
{
    public static void ApplyInfrascructureDependenciesConfiguration(
        this IServiceCollection serviceCollection,
        string npgsqlConnectionString)
    {
        #region Entity Framework Core DbContext Dependencies Configuration

        const int DATA_CONTEXT_MINIMUM_POOL_SIZE = 2048;
        const string DATA_MIGRATIONS_ASSEMBLY_NAME = "OVB.Demos.InvestmentPortfolio.Infrascructure";

        serviceCollection.AddDbContextPool<DataContext>(
            optionsAction: options => options.UseNpgsql(
                connectionString: npgsqlConnectionString,
                npgsqlOptionsAction: npgsqlOptions => npgsqlOptions.MigrationsAssembly(
                    assemblyName: DATA_MIGRATIONS_ASSEMBLY_NAME)),
            poolSize: DATA_CONTEXT_MINIMUM_POOL_SIZE);

        #endregion

        #region Entity Framework Core UnitOfWork Dependencies Configuration

        serviceCollection.AddScoped<IUnitOfWork, DefaultUnitOfWork>();

        #endregion

        #region Entity Framework Core Repositories Configuration

        serviceCollection.AddScoped<IExtensionOrderRepository, OrderRepository>();
        serviceCollection.AddScoped<IBaseRepository<Order>, OrderRepository>();

        serviceCollection.AddScoped<IExtensionCustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<IBaseRepository<Customer>, CustomerRepository>();

        serviceCollection.AddScoped<IExtensionOperatorRepository, OperatorRepository>();
        serviceCollection.AddScoped<IBaseRepository<Operator>, OperatorRepository>();

        serviceCollection.AddScoped<IExtensionExtractRepository, ExtractRepository>();
        serviceCollection.AddScoped<IBaseRepository<Extract>, ExtractRepository>();

        serviceCollection.AddScoped<IExtensionFinancialAssetRepository, FinancialAssetRepository>();
        serviceCollection.AddScoped<IBaseRepository<FinancialAsset>, FinancialAssetRepository>();

        serviceCollection.AddScoped<IExtensionPortfolioRepository, PortfolioRepository>();
        serviceCollection.AddScoped<IBaseRepository<Portfolio>, PortfolioRepository>();

        #endregion
    }
}
