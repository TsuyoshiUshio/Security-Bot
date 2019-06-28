using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace SecurityBot.Provider.SonarCloud
{
    public class SonarCloudStartup : IProviderStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<IRestClientContext>(GetRestClientContext());
            builder.Services.AddSingleton<ISonarCloudRepository, SonarCloudRepository>();
        }

        private IRestClientContext GetRestClientContext()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", SonarCloudConfiguration.Pat, ""))));
            return new RestClientContext(client);
        }
    }
}
