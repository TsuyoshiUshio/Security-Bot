using System;
using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace SecurityBot.Provider.AzureDevOps
{
    public class AzureDevOpsStartup : IProviderStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            VssConnection connection = CreateVssConnection();
            builder.Services.AddSingleton<IAzureDevOpsWorkItemRepository>(
                new AzureDevOpsWorkItemRepository(GetWorkItemTrackingHttpClient(connection)));
        }

        private VssConnection CreateVssConnection()
        {
            var connection = new VssConnection(new Uri(AzureDevOpsConfiguration.OrganizationUrl), new VssBasicCredential(string.Empty, AzureDevOpsConfiguration.Pat));
            return connection;
        }

        internal virtual WorkItemTrackingHttpClientBase GetWorkItemTrackingHttpClient(VssConnection connection)
        {
            return connection.GetClient<WorkItemTrackingHttpClient>();
        }
    }
}
