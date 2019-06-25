using System;
using System.Collections.Generic;
using System.Text;
using SecurityBot.Provider.SonarCloud;
using Xunit;

namespace SecurityBot.Test.Provider.SonarCloud
{
    public class SonarCloudConfigurationTest
    {
        [Fact]
        public void ConfigrationNormalCase()
        {
            var expectedSonarCloudPat = "quux";

            Environment.SetEnvironmentVariable(SonarCloudConfiguration.SonarCloudPatSetting, expectedSonarCloudPat, EnvironmentVariableTarget.Process);
            Assert.Equal(expectedSonarCloudPat, SonarCloudConfiguration.Pat);
        }
    }
}
