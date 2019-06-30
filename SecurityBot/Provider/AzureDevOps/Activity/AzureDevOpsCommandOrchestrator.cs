using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SecurityBot.Command;
using SecurityBot.Model;

namespace SecurityBot.Provider.AzureDevOps.Activity
{
    public class AzureDevOpsCommandOrchestrator : ICommandOrchestratorWorkItemActivity
    {
        private IAzureDevOpsWorkItemRepository _workItemRepository;
        private const string ProviderSection = "_" + AzureDevOpsConfiguration.ProviderName;
        public AzureDevOpsCommandOrchestrator(IAzureDevOpsWorkItemRepository repository)
        {
            _workItemRepository = repository;
        }

        [FunctionName(nameof(CommandOrchestrator) + ProviderSection + "_CreateWorkItem")]
        public async Task<WorkItem> CreateWorkItemAsync([ActivityTrigger] CreateWorkItemContext context)
        {
            var body = $"<b>{context.Issue.Type}</b></p>{context.Issue.Message}</p>See more <a href=\"{context.Issue.Url}\">details</a>.";
            var workItemSource = new WorkItemSource()
            {
                Title =
                    $"{context.CreatedReviewComment.ScanProvider} Issue {BotConfiguration.RepositoryProvider} PR {context.PullRequestId}: {context.Issue.Message}",
                Description = body
            };
            var workItem = await _workItemRepository.CreateWorkItem(workItemSource);
            return new WorkItem()
            {
                Id = workItem.Id.ToString(),
                Uri = ((Microsoft.VisualStudio.Services.WebApi.ReferenceLink)workItem.Links.Links["html"]).Href,
                WorkItemProvider = AzureDevOpsConfiguration.ProviderName
            };

        }
    }
}
