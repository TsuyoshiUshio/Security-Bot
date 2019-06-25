using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs;

namespace SecurityBot
{
    /// <summary>
    /// An interface to enable Providers to configure DI for Azure Functions.
    /// <see cref="ProviderRegistry"/>
    /// </summary>
    public interface IProviderStartup
    {
        void Configure(IWebJobsBuilder builder);
    }
}
