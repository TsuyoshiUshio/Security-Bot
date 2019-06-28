using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SecurityBot.Entity;
using SecurityBot.Model;

namespace SecurityBot.Decorator
{
    public class DecorationOrchestrator
    {
        public const string SectionGetIssue = "_GetIssues";
        public const string SectionCreatePRReviewComment = "_CreatePRReviewComment";

        [FunctionName(nameof(DecorationOrchestrator))]
        public async Task Orchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var decoratorContext = context.GetInput<DecoratorContext>();
            // Get issues -> Activity to fetch the issues 
            foreach (var scanProvider in BotConfiguration.ScannerProviders)
            {
                var issues =
                    await context.CallActivityAsync<IEnumerable<Issue>>(nameof(DecorationOrchestrator) + "_" + scanProvider + SectionGetIssue,
                        decoratorContext);

                // Get state (CreateOrGetEntity)
                EntityStateResponse<PullRequestStateContext> response = await context.CallActivityAsync<EntityStateResponse<PullRequestStateContext>>(nameof(PullRequestEntity) + "_GetPullRequestStateContext", decoratorContext.Url.GetEntityId());
                var pullRequestStateContext = response.EntityState ?? new PullRequestStateContext()
                {
                    PullRequestId = decoratorContext.PullRequestId
                };

                var unCommentedIssue = issues.Where(issue => !pullRequestStateContext.CreatedReviewComment.Contains(new CreatedReviewComment() { IssueId = issue.Id })).ToList();

                // Create Comment with Issue only for not commented yet.
                foreach (var issue in unCommentedIssue)
                {
                    var createdReviewComment =
                        await context.CallActivityAsync<CreatedReviewComment>(
                            nameof(DecorationOrchestrator) + "_" + BotConfiguration.RepositoryProvider + SectionCreatePRReviewComment, new CreateReviewCommentContext()
                            {
                                Issue = issue,
                                DecoratorContext = decoratorContext
                            });
                    if (createdReviewComment != null)
                    {
                        pullRequestStateContext.Add(createdReviewComment);
                    }
                }

                await context.CallEntityAsync<PullRequestStateContext>(
                    new EntityId(nameof(PullRequestEntity), decoratorContext.Url.GetEntityId()), "update",
                    pullRequestStateContext);
            }
        }

    }

    /// <summary>
    /// Interface for Activities for this Orchestration.
    /// All the provider which use this Orchestration need to implement this Interface.
    /// </summary>
    public interface IDecorationOrchestratorScannerActivity
    {
        /// <summary>
        /// Get Issues from each scan providers. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        Task<IEnumerable<SecurityBot.Model.Issue>> GetIssuesAsync(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger logger);

    }

    public interface IDecorationOrchestratorRepositoryActivity
    {
        /// <summary>
        /// Create Comment for each uncommented issue. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        Task<CreatedReviewComment> CreateCommentsAsync(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger logger);
    }

}
