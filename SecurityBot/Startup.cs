using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using SecurityBot;

[assembly: WebJobsStartup(typeof(Startup))]
namespace SecurityBot
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            new ProviderRegistry().Register(builder);
            new CommandHookParserRegistry().Register(builder);
        }
    }
}
