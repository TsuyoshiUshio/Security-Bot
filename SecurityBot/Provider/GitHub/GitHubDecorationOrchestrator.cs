using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Octokit;
using SecurityBot.Decorator;
using SecurityBot.Model;

namespace SecurityBot.Provider.GitHub
{
    public class GitHubDecorationOrchestrator : IDecorationOrchestratorRepositoryActivity
    {
        private const string ProviderSection = "_" + GitHubConfiguration.ProviderName;
        private IGitHubRepository _repository;
        private IGitHubRepositoryContext _context;

        public GitHubDecorationOrchestrator(IGitHubRepository repository, IGitHubRepositoryContext context)
        {
            _repository = repository;
            _context = context;
        }

        [FunctionName(nameof(DecorationOrchestrator) + ProviderSection + DecorationOrchestrator.SectionCreatePRReviewComment)]
        public async Task<CreatedReviewComment> CreateCommentsAsync(IDurableActivityContext context, ILogger logger)
        {

            var commentContext = context.GetInput<CreateReviewCommentContext>();

            
            if (commentContext.Issue.Path != null)
            {
                PullRequestReviewComment result = await _repository.CreatePullRequestReviewComment(
                    new Comment
                    {
                        Body = $"**{commentContext.Issue.Type}**\n> {commentContext.Issue.Message}\n See [details]({commentContext.Issue.Url})",
                        RepositoryOnwer = _context.Owner,
                        RepositoryName = _context.Name,
                        CommitId = commentContext.DecoratorContext.CommitId,
                        Path = commentContext.Issue.Path,
                        Position = 5,
                        PullRequestId = commentContext.DecoratorContext.PullRequestId
                    });
                return new CreatedReviewComment()
                {
                    IssueId = commentContext.Issue.Id,
                    CommentId = result.Id.ToString(),
                    ScanProvider = commentContext.ScanProvider,
                    Tag = commentContext.DecoratorContext.Tag
                };
            }
            else
            {
                // TODO there are issues that code agnostic one. Research how to do it . 
                return null;
            }
        }
    }
}
