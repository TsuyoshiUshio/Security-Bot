using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace SecurityBot.Provider.GitHub
{
    public interface IGitHubRepository
    {
        Task<PullRequestReviewComment> CreatePullRequestReviewComment(Comment comment);
        Task<PullRequestReviewComment> GetSingleComment(int commentId);
        Task<IReadOnlyList<PullRequestReviewComment>> GetPullRequestReviewComments(int pullRequestId);
        IGitHubRepositoryContext GitHubRepositoryContext { get; }
        Task<PullRequestReviewComment> CreatePullRequestReplyComment(int pullRequestNumber, string body, int inReplyTo);
        Task<IssueComment> CreatePullRequestIssueComment(int pullRequestNumber, string body);
    }


    public class GitHubRepository : IGitHubRepository
    {
        internal IGitHubClient client; // internal for testability
        internal IGitHubRepositoryContext context;
        public GitHubRepository(IGitHubClient client, IGitHubRepositoryContext context)
        {
            this.client = client;
            this.context = context;
        }

        public IGitHubRepositoryContext GitHubRepositoryContext => context;

        public async Task<PullRequestReviewComment> CreatePullRequestReviewComment(Comment comment)
        {
            try
            {
                var reviewComment =
                    new PullRequestReviewCommentCreate(comment.Body, comment.CommitId, comment.Path, comment.Position);
                var a = await client.PullRequest.ReviewComment.Create(comment.RepositoryOnwer, comment.RepositoryName,
                    int.Parse(comment.PullRequestId), reviewComment);
                return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<PullRequestReviewComment> CreatePullRequestReplyComment(int pullRequestNumber, string body, int inReplyTo)
        {
            var replyComment = new PullRequestReviewCommentReplyCreate(body, inReplyTo);
            return client.PullRequest.ReviewComment.CreateReply(context.Owner, context.Name, pullRequestNumber, replyComment);
        }

        public Task<PullRequestReviewComment> GetSingleComment(int commentId)
        {
            return client.PullRequest.ReviewComment.GetComment(context.Owner, context.Name, commentId);
        }

        public Task<IReadOnlyList<PullRequestReviewComment>> GetPullRequestReviewComments(int pullRequestId)
        {
            return client.PullRequest.ReviewComment.GetAll(context.Owner, context.Name, pullRequestId);
        }

        public Task<IssueComment> CreatePullRequestIssueComment(int pullRequestNumber, string body)
        {
            return client.Issue.Comment.Create(context.Owner, context.Name, pullRequestNumber, body);
        }

    }
}
