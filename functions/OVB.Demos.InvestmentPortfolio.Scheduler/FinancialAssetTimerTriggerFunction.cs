using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace OVB.Demos.InvestmentPortfolio.Scheduler;

public class FinancialAssetTimerTriggerFunction
{
    const string EXPIRATION_FINANCIAL_ASSET_ADVICE_CRON_TAB_FUNCTION_NAME = "FinancialAssetExpirationDailyAdvice";
    const string EXPIRATION_DAILY_ADVICE_CRON_TAB_EXPRESSION = "45 34 15 * * *";

    [FunctionName(EXPIRATION_FINANCIAL_ASSET_ADVICE_CRON_TAB_FUNCTION_NAME)]
    public void Run([TimerTrigger(EXPIRATION_DAILY_ADVICE_CRON_TAB_EXPRESSION)] TimerInfo myTimer, 
        ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    }
}
