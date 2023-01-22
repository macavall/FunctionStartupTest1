using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(FunctionStartupTest1.Startup))]

namespace FunctionStartupTest1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<MyOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("MyOptions").Bind(settings);
            });

            // add singleton
            builder.Services.AddSingleton<IMyService, MyService>();
        }
    }

    public class MyOptions
    {
        public string MyCustomSetting { get; set; }
    }

    // create interface
    public interface IMyService
    {
        string GetMyCustomSetting();
    }

    public class MyService : IMyService
    {
        private readonly MyOptions _options;

        public MyService(MyOptions options)
        {
            _options = options;
        }

        public string GetMyCustomSetting()
        {
            return _options.MyCustomSetting;
        }
    }

}
