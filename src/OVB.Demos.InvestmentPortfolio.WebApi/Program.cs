using OVB.Demos.InvestmentPortfolio.Infrascructure;
using OVB.Demos.InvestmentPortfolio.Application;

namespace OVB.Demos.InvestmentPortfolio.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.ApplyInfrascructureDependenciesConfiguration(
            npgsqlConnectionString: string.Empty);

        builder.Services.ApplyApplicationDependenciesConfiguration(
            jwtBearerIssuerSigningKey: builder.Configuration["Application:JwtBearerIssuerPrivateKey"] ?? throw new Exception("builder.Configuration[\"Application:JwtBearerIssuerPrivateKey\"]"),
            passwordHashPrivateKey: builder.Configuration["Application:SHA256PrivateKey"] ?? throw new Exception("builder.Configuration[\"Application:SHA256PrivateKey\"]"));

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
