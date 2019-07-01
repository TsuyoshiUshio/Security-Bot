using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SecurityBot.Command;
using SecurityBot.Model;

namespace SecurityBot.Provider.SonarCloud.Activity
{
    public class SonarCloudCommandOrchestrator : ICommandOrchestratorScannerActivity
    {
        private const string ProviderSection = "_" + SonarCloudConfiguration.ProviderName;
        private readonly ISonarCloudRepository _repository;

        public SonarCloudCommandOrchestrator(ISonarCloudRepository repository)
        {
            _repository = repository;
        }


        [FunctionName(nameof(CommandOrchestrator) + ProviderSection + "_GetIssue" )]
        public async Task<Issue> GetIssueAsync([ActivityTrigger] IDurableActivityContext context, ILogger logger)
        {
            var getIssueContext = context.GetInput<GetIssueContext>();
            var parentCreatedReviewComment = getIssueContext.CreatedReviewComment;


            var projectKey = parentCreatedReviewComment.Tag.GetValueOrDefault(SonarCloudConfiguration.ProjectKey);

            var searchIssue = await _repository.GetIssues(getIssueContext.PullRequestId,
                projectKey, parentCreatedReviewComment.IssueId);

            return searchIssue.issues.Select(p => new Issue()
                {
                    Id = p.key,
                    Type = p.type,
                    Message = p.message,
                    Url = $"https://sonarcloud.io/project/issues?id={projectKey}&open={p.key}&pullRequest={getIssueContext.PullRequestId}&resolved=false",
                    Provider = SonarCloudConfiguration.ProviderName
            }).FirstOrDefault();
        }
    }
}
