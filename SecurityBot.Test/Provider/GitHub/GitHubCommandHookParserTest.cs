using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using SecurityBot.Command;
using SecurityBot.Model;
using SecurityBot.Provider.GitHub;
using Xunit;

namespace SecurityBot.Test.Provider.GitHub
{
    public class GitHubCommandHookParserTest
    {
        [Fact]
        public async Task ParseNormalCase()
        {
            var expectedCommandName = CommandRouter.CreateWorkItemCommand;
            var expectedCommentId = "3";

            var expectedBody = "/workItem";
            var expectedReplyToId = "4";
            var expectedPullRequestUrl = "https://github.com/foo/bar/pull/5";
            var expectedPullRequestId = "5";
            var expectedRepositoryOwner = "foo";
            var expectedRepositoryName = "bar";

            string inputBody = GetInputBody(
                expectedCommentId,
                expectedBody,
                expectedReplyToId,
                expectedPullRequestUrl,
                expectedPullRequestId,
                expectedRepositoryOwner,
                expectedRepositoryName);

            var httpMock = GetMockHttpRequest(inputBody);

            var parser = new GitHubCommandHookParser();
            CommandHookContext context = await parser.Parse(httpMock.Object);

            Assert.Equal(expectedCommandName, context.CommandName);
            Assert.Equal(expectedCommentId, context.Id);
            Assert.Equal(expectedReplyToId, context.ReplyToId);
            Assert.Equal(expectedPullRequestUrl, context.PullRequestUri.AbsoluteUri);
            Assert.Equal(expectedPullRequestId, context.PullRequestId);
            Assert.Equal(expectedRepositoryName, context.RepositoryName);
            Assert.Equal($"{expectedRepositoryOwner}/{expectedRepositoryName}", context.RepositoryFullName);
        }

        [Fact]
        public async Task IgnoreIfItIsNotCommand()
        {
            var expectedCommentId = "3";

            var expectedBody = "Hello guys.";
            var expectedReplyToId = "4";
            var expectedPullRequestUrl = "https://github.com/foo/bar/pull/5";
            var expectedPullRequestId = "5";
            var expectedRepositoryOwner = "foo";
            var expectedRepositoryName = "bar";

            string inputBody = GetInputBody(
                expectedCommentId,
                expectedBody,
                expectedReplyToId,
                expectedPullRequestUrl,
                expectedPullRequestId,
                expectedRepositoryOwner,
                expectedRepositoryName);

            var httpMock = GetMockHttpRequest(inputBody);

            var parser = new GitHubCommandHookParser();
            CommandHookContext context = await parser.Parse(httpMock.Object);

            Assert.Null(context);
        }

        private Mock<HttpRequest> GetMockHttpRequest(string inputBody)
        {
            byte[] messageBodyArray = Encoding.ASCII.GetBytes(inputBody);
            var httpMock = new Mock<HttpRequest>();
            httpMock.Setup(p => p.Body).Returns(new MemoryStream(messageBodyArray));
            return httpMock;
        }

        private string GetInputBody(string commentId, string body, string inReplyToId, string pullRequestHtmlUrl, string pullRequestNumber, string repositoryOwner, string repositoryName)
        {
            return $@"
{{
                ""action"": ""created"",
    ""comment"": {{
                ""id"": {commentId},
                ""body"": ""{body}"",
                ""in_reply_to_id"": {inReplyToId},
            }},
            ""pull_request"": {{
                ""html_url"": ""{pullRequestHtmlUrl}"",
                ""number"": {pullRequestNumber}
            }},
            ""repository"": {{
                ""name"": ""{repositoryName}"",
                ""full_name"": ""{repositoryOwner}/{repositoryName}""
            }}
}}
";
        }
    }
}
