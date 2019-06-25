using System;
using System.Collections.Generic;
using System.Text;
using SecurityBot.Provider.GitHub;
using Xunit;

namespace SecurityBot.Test.Provider.GitHub
{
    public class GitHubConfigurationTest
    {
        [Fact]
        public void ConfigurationNormalCase()
        {
            var expectedGitHubPat = "foo";
            var expectedGitHubOwner = "bar";

            Environment.SetEnvironmentVariable(GitHubConfiguration.GitHubPatSetting, expectedGitHubPat, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(GitHubConfiguration.GitHubOwnerSetting, expectedGitHubOwner, EnvironmentVariableTarget.Process);

            Assert.Equal(expectedGitHubPat, GitHubConfiguration.Pat);
            Assert.Equal(expectedGitHubOwner, GitHubConfiguration.Owner);
        }

    }
}
