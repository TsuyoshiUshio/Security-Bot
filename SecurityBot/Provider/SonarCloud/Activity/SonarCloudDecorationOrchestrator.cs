using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SecurityBot.Model;
using Microsoft.Extensions.Logging;
using SecurityBot.Decorator;
using SecurityBot.Provider.SonarCloud;

namespace SecurityBot.Provider.SonarCloud.Activity
{
    /// <summary>
    /// Implement DecorationOrchestrator Activity for SonarCloud
    /// </summary>
    public class SonarCloudDecorationOrchestrator : IDecorationOrchestratorScannerActivity
    {
        private readonly ISonarCloudRepository _repository;

        public SonarCloudDecorationOrchestrator(ISonarCloudRepository repository)
        {
            _repository = repository;
        }

        private const string ProviderSection = "_" + SonarCloudConfiguration.ProviderName;

        [FunctionName(nameof(SonarCloudDecorationOrchestrator) + ProviderSection + "_GetIssues")]
        public async Task<IEnumerable<SecurityBot.Model.Issue>> GetIssuesAsync(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger logger)
        {
            var decorationContext = context.GetInput<DecoratorContext>();

            var searchIssue = await _repository.GetIssues(decorationContext.PullRequestId,
                decorationContext.Tag.GetValueOrDefault(SonarCloudConfiguration.ProjectKey));
            var issues = new List<Issue>();
            foreach (var issue in searchIssue.issues)
            {
                var projectKey = decorationContext.Tag.GetValueOrDefault(SonarCloudConfiguration.ProjectKey);
                var path = issue.component.Replace($"{projectKey}:", "");
                var newIssue = new Issue()
                {
                    Id = issue.key,
                    Type = issue.type,
                    Message = issue.message,
                    Url = $"https://sonarcloud.io/project/issues?id={projectKey}&open={issue.key}&pullRequest={decorationContext.PullRequestId}&resolved=false",
                    Provider = SonarCloudConfiguration.ProviderName
                };

                newIssue.Path = path != projectKey ? path : null;
                issues.Add(newIssue);
            }
            return issues;
        }

    }
}
