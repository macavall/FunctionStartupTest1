using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FunctionStartupTest1
{
    public class Function1
    {
        private readonly MyOptions _settings;

        public Function1(IOptions<MyOptions> options)
        {
            _settings = options.Value;
        }

        [FunctionName("Function1")]
        public void Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            log.LogInformation($"Options Value: {_settings.MyCustomSetting } ");
        }
    }
}
