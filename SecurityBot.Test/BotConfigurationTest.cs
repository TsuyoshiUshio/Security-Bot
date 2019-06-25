using System;
using System.Linq;
using Microsoft.VisualStudio.Services.OAuth;
using Xunit;

namespace SecurityBot.Test
{
    public class BotConfigurationTest
    {
        [Fact]
        public void GetConfigurations()
        {
            var expectedRepositoryProvider = "GitHub";
            var expectedWorkItemProvider = "AzureDevOps";
            var inputScannerProviders = "SonarCloud,Aqua";
            var expectedScannerProvider01 = "SonarCloud";
            var expectedScannerProvider02 = "Aqua";

            Environment.SetEnvironmentVariable(BotConfiguration.RepositoryProviderSetting, expectedRepositoryProvider, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(BotConfiguration.WorkItemProviderSetting, expectedWorkItemProvider, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(BotConfiguration.ScannerProviderSetting, inputScannerProviders, EnvironmentVariableTarget.Process);

            Assert.Equal(expectedRepositoryProvider, BotConfiguration.RepositoryProvider);
            Assert.Equal(expectedWorkItemProvider, BotConfiguration.WorkItemProvider );
            Assert.Equal(expectedScannerProvider01, BotConfiguration.ScannerProviders.First());
            Assert.Equal(expectedScannerProvider02, BotConfiguration.ScannerProviders.Last());

        }
    }
}
