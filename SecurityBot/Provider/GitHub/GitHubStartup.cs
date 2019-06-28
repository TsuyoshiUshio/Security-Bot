using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace SecurityBot.Provider.GitHub
{
    public class GitHubStartup : IProviderStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<IGitHubRepositoryContext>(GetGitHubRepositoryContext());
            builder.Services.AddSingleton<IGitHubClient>(GetGitHubClient());
        }

        private IGitHubRepositoryContext GetGitHubRepositoryContext()
        {
            return new GitHubRepositoryContext()
            {
                Owner = GitHubConfiguration.Owner,
                Name = GitHubConfiguration.Name // TODO This will be removed from the configuration.
            };
        }

        private IGitHubClient GetGitHubClient()
        {
            var client = new GitHubClient(new ProductHeaderValue("PullRequestBot"));
            var tokenAuth = new Credentials(GitHubConfiguration.Owner, GitHubConfiguration.Pat);
            client.Credentials = tokenAuth;
            return client;
        }
    }
}
