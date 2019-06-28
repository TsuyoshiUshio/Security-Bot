using System;
using System.Linq;
using Microsoft.VisualStudio.Services.OAuth;
using Xunit;

namespace SecurityBot.Test
{
    [CollectionDefinition("ConfigurationTest", DisableParallelization = true)]
    public class ConfigurationTestCollection
    {
    }

    [Collection("ConfigurationTest")]
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

            // Testing with EnvironmentValuables should only reside in here. 
            // Multi threading test will share it. 
            Environment.SetEnvironmentVariable(BotConfiguration.RepositoryProviderSetting, expectedRepositoryProvider, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(BotConfiguration.WorkItemProviderSetting, expectedWorkItemProvider, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(BotConfiguration.ScannerProviderSetting, inputScannerProviders, EnvironmentVariableTarget.Process);

            Assert.Equal(expectedRepositoryProvider, BotConfiguration.RepositoryProvider);
            Assert.Equal(expectedWorkItemProvider, BotConfiguration.WorkItemProvider );

            Environment.SetEnvironmentVariable(BotConfiguration.ScannerProviderSetting, inputScannerProviders, EnvironmentVariableTarget.Process);
            Assert.Equal(expectedScannerProvider01, BotConfiguration.ScannerProviders.First());
            Assert.Equal(expectedScannerProvider02, BotConfiguration.ScannerProviders.Last());

        }
    }
}
