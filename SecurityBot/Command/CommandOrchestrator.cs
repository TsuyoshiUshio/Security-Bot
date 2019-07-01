using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Octokit;
using SecurityBot.Decorator;
using SecurityBot.Entity;
using SecurityBot.Model;
using Issue = SecurityBot.Model.Issue;

namespace SecurityBot.Command
{
    public class CommandOrchestrator
    {
        [FunctionName(nameof(CommandOrchestrator))]
        public async Task Orchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var commandHookContext = context.GetInput<CommandHookContext>();

            var response = await context.CallActivityAsync<EntityStateResponse<PullRequestStateContext>>(nameof(PullRequestEntity) + "_GetPullRequestStateContext", commandHookContext.PullRequestUri.GetEntityId());
 
            // Ignore if the Decoration has not finished.
            if (response.EntityExists)
            {
                PullRequestStateContext pullRequestStateContext = response.EntityState;
                switch (commandHookContext.CommandName)
                {
                    case CommandRouter.CreateWorkItemCommand:
                        // GetIssue that match Scan Provider. You can find the ScanProvider from response.
                        CreatedReviewComment parentReviewComment =
                            pullRequestStateContext.CreatedReviewComment.FirstOrDefault(p =>
                                p.CommentId.ToString() == commandHookContext.ReplyToId);
                        var issue = await context.CallActivityAsync<Issue>(
                            nameof(CommandOrchestrator) + "_"+ parentReviewComment.ScanProvider + "_GetIssue",
                            new GetIssueContext()
                            {
                                CreatedReviewComment = parentReviewComment,
                                PullRequestId = pullRequestStateContext.PullRequestId
                            });

                        // Create WorkItem that match WorkItem Provider
                        var workItem = await context.CallActivityAsync<WorkItem>(
                            nameof(CommandOrchestrator) + "_" + BotConfiguration.WorkItemProvider + "_CreateWorkItem",
                            new CreateWorkItemContext()
                            {
                                Issue = issue,
                                CreatedReviewComment = parentReviewComment,
                                PullRequestId = pullRequestStateContext.PullRequestId
                            });

                        // Create Comment that match Repository Provider
                        await context.CallActivityAsync(
                            nameof(CommandOrchestrator) + "_" + BotConfiguration.RepositoryProvider +
                            "_CreateWorkItemReplyComment", new CreateWorkItemReplyCommentContext()
                            {
                                WorkItem = workItem,
                                InReplyTo = parentReviewComment.CommentId,
                                PullRequestId = pullRequestStateContext.PullRequestId
                            });
                        // Update the PullRequestStateContext
                        pullRequestStateContext.Add(new CreatedWorkItem()
                        {
                            CommentId = parentReviewComment.CommentId,
                        });

                        break;
                    case CommandRouter.SuppressFalsePositiveCommand:
                        // GetIssue that match Scan Provider
                        // Update the Issue state
                        // Create Comment that match Repository Provider
                        break;
                    default:
                        // Create Sorry Comment 
                        await context.CallActivityAsync(nameof(CommandOrchestrator) + "_" + BotConfiguration.RepositoryProvider +
                                                        "_CreateSimpleReplyComment", new CreateSimpleReplyCommentContext()
                        {
                            Body = $"Command [{commandHookContext.CommandName}] hasn't been supported. Ask bot administrator.",
                            InReplyTo = commandHookContext.ReplyToId,
                            PullRequestId = commandHookContext.PullRequestId
                        });
                        break;
                }
                // Update the CurrentPullRequestStatus
                await context.CallEntityAsync<PullRequestStateContext>(
                    new EntityId(nameof(PullRequestEntity), commandHookContext.PullRequestUri.GetEntityId()), "update",
                    pullRequestStateContext);
            }
            else
            {
                await context.CallActivityAsync(nameof(CommandOrchestrator) + "_" + BotConfiguration.RepositoryProvider +
                                                "_CreateSimpleReplyComment", new CreateSimpleReplyCommentContext()
                {
                    Body = "Security Decoration seems not finished. Wait until the PullRequest validation CI has done.",
                    InReplyTo = commandHookContext.ReplyToId,
                    PullRequestId = commandHookContext.PullRequestId
                });
            }
        }

    }

    public interface ICommandOrchestratorScannerActivity
    {
        /// <summary>
            /// Get Issues from each scan providers. 
            /// </summary>
            /// <param name="context">include <see cref="GetIssueContext"/></param>
            /// <param name="logger"></param>
            /// <returns></returns>
            Task<SecurityBot.Model.Issue> GetIssueAsync(
                [ActivityTrigger] IDurableActivityContext context,
                ILogger logger);
    }

    public interface ICommandOrchestratorWorkItemActivity
    {
        /// <summary>
        /// Create WorkItem with work item provider
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<WorkItem> CreateWorkItemAsync(
            [ActivityTrigger] CreateWorkItemContext context);
    }

    public interface ICommandOrchestratorRepositoryActivity
    {
        /// <summary>
        /// CreateReplyComment for WorkItem
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task CreateWorkItemReplyComment(
            [ActivityTrigger] CreateWorkItemReplyCommentContext context);

        /// <summary>
        /// Create reply comment with body.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task CreateSimpleReplyComment(
            [ActivityTrigger] CreateSimpleReplyCommentContext context);
    }
}
