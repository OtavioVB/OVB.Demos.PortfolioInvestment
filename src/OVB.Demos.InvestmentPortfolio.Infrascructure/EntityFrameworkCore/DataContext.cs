using Microsoft.EntityFrameworkCore;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.CustomerContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.ExtractContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.FinancialAssetContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OrderContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.PortfolioContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Mappings;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore;

public class DataContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Extract> Extracts { get; set; }
    public virtual DbSet<Operator> Operators { get; set; }
    public virtual DbSet<FinancialAsset> FinancialAssets { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Portfolio> Portfolios { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OperatorMapping());
        modelBuilder.ApplyConfiguration(new CustomerMapping());
        modelBuilder.ApplyConfiguration(new FinancialAssetMapping());
        modelBuilder.ApplyConfiguration(new ExtractMapping());
        modelBuilder.ApplyConfiguration(new OrderMapping());
        modelBuilder.ApplyConfiguration(new PortfolioMapping());
        base.OnModelCreating(modelBuilder);
    }
}
