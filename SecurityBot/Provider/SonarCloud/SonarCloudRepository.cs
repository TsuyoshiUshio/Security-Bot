using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecurityBot.Provider.SonarCloud.Generated.DoTransition;
using SecurityBot.Provider.SonarCloud.Generated.SearchIssue;

namespace SecurityBot.Provider.SonarCloud
{
    public interface ISonarCloudRepository
    {
        Task<SearchIssue> GetIssues(string pullRequestId, string projectKey);
        Task<SearchIssue> GetIssues(string pullRequestId, string projectKey, string issueKey);
        Task<DoTransition> DoTransition(string issueKey, string transition);
    }

    public class SonarCloudRepository : ISonarCloudRepository
    {
        internal IRestClientContext context; // internal for testability
        public SonarCloudRepository(IRestClientContext context)
        {
            this.context = context;
        }

        public Task<SearchIssue> GetIssues(string pullRequestId, string projectKey)
        {
            return context.GetAsync<SearchIssue>($"https://sonarcloud.io/api/issues/search?pullRequest={pullRequestId}&projects={projectKey}");
        }

        public Task<SearchIssue> GetIssues(string pullRequestId, string projectKey, string issueKey)
        {
            return context.GetAsync<SearchIssue>($"https://sonarcloud.io/api/issues/search?pullRequest={pullRequestId}&projects={projectKey}&issues={issueKey}");
        }

        public async Task<DoTransition> DoTransition(string issueKey, string transition)
        {
            var result = await context.PostAsync<string, DoTransition>(
                $"https://sonarcloud.io/api/issues/do_transition?issue={issueKey}&transition={transition}", null);
            return result;
        }
    }
}
