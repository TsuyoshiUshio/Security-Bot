using System;
using System.Collections.Generic;
using System.Text;
using SecurityBot.Provider.AzureDevOps;
using Xunit;

namespace SecurityBot.Test.Provider.AzureDevOps
{
    public class AzureDevOpsConfigurationTest
    {
        [Fact]
        public void ConfigurationNormalCase()
        {
            var expectedAzureDevOpsPat = "baz";
            var expectedAzureDevOpsOrganizationUrl = "https://dev.azure.qux";

            Environment.SetEnvironmentVariable(AzureDevOpsConfiguration.AzureDevOpsPatSetting, expectedAzureDevOpsPat, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(AzureDevOpsConfiguration.AzureDevOpsOrganizationUrlSetting, expectedAzureDevOpsOrganizationUrl, EnvironmentVariableTarget.Process);

            Assert.Equal(expectedAzureDevOpsPat, AzureDevOpsConfiguration.Pat);
            Assert.Equal(expectedAzureDevOpsOrganizationUrl, AzureDevOpsConfiguration.OrganizationUrl);

        }
    }
}
