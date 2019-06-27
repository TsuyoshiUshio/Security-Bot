using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SecurityBot.Decorator;
using SecurityBot.Model;

namespace SecurityBot.Provider.AzureDevOps.CI
{
    public class AzureDevOpsDecorator : DecoratorBase
    {
        public override void Update(DecoratorContext context, HttpRequest request)
        {
            string pullRequestId = request.Query["pullRequestId"];
            string commitId = request.Query["commitId"];
            string pullRequestUrl = request.Query["pullRequestUrl"];
            context.PullRequestId = pullRequestId;
            context.CommitId = commitId;
            context.Url = new Uri(pullRequestUrl);
        }
    }
}
