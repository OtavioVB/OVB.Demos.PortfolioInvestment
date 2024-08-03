using OVB.Demos.InvestmentPortfolio.Infrascructure;
using OVB.Demos.InvestmentPortfolio.Application;
using Npgsql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            npgsqlConnectionString: builder.Configuration["Infrascructure:Database:NpgsqlConnectionString"] ?? throw new ArgumentNullException(nameof(NpgsqlConnectionStringBuilder)));

        string JWT_BEARER_ISSUER_SIGNING_KEY = builder.Configuration["Application:JwtBearerIssuerPrivateKey"] ?? throw new ArgumentNullException(nameof(Application.DependencyInjection.ApplyApplicationDependenciesConfiguration));

        builder.Services.ApplyApplicationDependenciesConfiguration(
            jwtBearerIssuerSigningKey: JWT_BEARER_ISSUER_SIGNING_KEY,
            passwordHashPrivateKey: builder.Configuration["Application:SHA256PrivateKey"] ?? throw new ArgumentNullException(nameof(Application.DependencyInjection.ApplyApplicationDependenciesConfiguration)));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    key: Encoding.UTF8.GetBytes(JWT_BEARER_ISSUER_SIGNING_KEY)),
            };
        });

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
