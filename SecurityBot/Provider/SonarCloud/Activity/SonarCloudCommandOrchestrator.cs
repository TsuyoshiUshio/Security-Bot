using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SecurityBot.Command;
using SecurityBot.Model;
using SecurityBot.Provider.SonarCloud.Generated.DoTransition;
using Issue = SecurityBot.Model.Issue;

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
                    Url = $"https://sonarcloud.io/project/issues?id={projectKey}&open={p.key}&pullRequest={getIssueContext.PullRequestId}",
                    Provider = SonarCloudConfiguration.ProviderName
            }).FirstOrDefault();
        }

        [FunctionName(nameof(CommandOrchestrator) + ProviderSection + "_TransitIssue")]
        public async Task TransitIssue([ActivityTrigger] TransitIssueContext context, ILogger logger)
        {
            var parentCreatedReviewComment = context.CreatedReviewComment;
            var result = await _repository.DoTransition(parentCreatedReviewComment.IssueId, context.Transition);
            logger.LogInformation("command: *******");
            logger.LogInformation(JsonConvert.SerializeObject(result));
        }
    }
}
