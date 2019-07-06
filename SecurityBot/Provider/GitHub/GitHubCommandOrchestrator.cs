using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SecurityBot.Command;
using SecurityBot.Model;

namespace SecurityBot.Provider.GitHub
{
    public class GitHubCommandOrchestrator : ICommandOrchestratorRepositoryActivity
    {
        private IGitHubRepository _repository;
        private const string ProviderSection = "_" + GitHubConfiguration.ProviderName;

        public GitHubCommandOrchestrator(IGitHubRepository repository)
        {
            _repository = repository;
        }

        [FunctionName(nameof(CommandOrchestrator) + ProviderSection +
                      "_CreateWorkItemReplyComment")]
        public Task CreateWorkItemReplyComment(
            [ActivityTrigger] CreateWorkItemReplyCommentContext context)
        {
            var body = $"WorkItem {context.WorkItem.Id} Created see [workItem]({context.WorkItem.Uri}).";

            return _repository.CreatePullRequestReplyComment(int.Parse(context.PullRequestId), body,
                int.Parse(context.InReplyTo));
        }

        [FunctionName(nameof(CommandOrchestrator) + ProviderSection +
                      "_CreateIssueTransitionReplyComment")]
        public Task CreateIssueTransitionReplyComment(
            [ActivityTrigger] CreateIssueTransitionReplyCommentContext context)
        {
            var body = $"The issue marked as {CommandRouter.GetTransition(context.Command)}. For more [detail]({context.Issue.Url}).";

            return _repository.CreatePullRequestReplyComment(int.Parse(context.PullRequestId), body,
                int.Parse(context.InReplyTo));
        }


        [FunctionName(nameof(CommandOrchestrator) + ProviderSection +
                      "_CreateSimpleReplyComment")]
        public Task CreateSimpleReplyComment(
            [ActivityTrigger] CreateSimpleReplyCommentContext context)
        {
            return _repository.CreatePullRequestReplyComment(int.Parse(context.PullRequestId), context.Body,
                int.Parse(context.InReplyTo));
        }
    }
}
