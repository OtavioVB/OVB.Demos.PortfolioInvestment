using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace OVB.Demos.InvestmentPortfolio.Scheduler;

public class FinancialAssetTimerTriggerFunction
{
    const string EXPIRATION_FINANCIAL_ASSET_ADVICE_CRON_TAB_FUNCTION_NAME = "FinancialAssetExpirationDailyAdvice";
    const string EXPIRATION_DAILY_ADVICE_CRON_TAB_EXPRESSION = "45 34 15 * * *";

    [FunctionName(EXPIRATION_FINANCIAL_ASSET_ADVICE_CRON_TAB_FUNCTION_NAME)]
    public async Task Run([TimerTrigger(EXPIRATION_DAILY_ADVICE_CRON_TAB_EXPRESSION)] TimerInfo myTimer, 
        ILogger log)
    {
        const string ENDPOINT = "https://localhost:5000/api/v1/financial-assets/advices";
        const string ADMINISTRATOR_REQUIRED_HEADER_KEY_NAME = "X-Administrator-Key";
        const string ADMINISTRATOR_KEY = "XLkNQ8h23I73KV8KXw1UrdyiKVhJi7yg";

        using var httpClient = HttpClientFactory.Create();

        httpClient.DefaultRequestHeaders.Add(
            name: ADMINISTRATOR_REQUIRED_HEADER_KEY_NAME,
            value: ADMINISTRATOR_KEY);

        var response = await httpClient.PostAsync(
            requestUri: ENDPOINT,
            content: null);

        if (response.IsSuccessStatusCode)
            log.LogInformation($"A trigger de envio de aviso de vencimento próximo dos ativos financeiros foram realizados com sucesso às: {DateTime.UtcNow}");

        log.LogError($"A trigger de envio de aviso de vencimento próximo dos ativos financeiros não foram realizados com sucesso às: {DateTime.UtcNow}");
    }
}
