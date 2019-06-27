using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SecurityBot.Decorator;
using SecurityBot.Model;

namespace SecurityBot
{
    public class ServiceEndpoint
    {
        [FunctionName("CIHook")]
        public async Task<IActionResult> CIHook(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        [OrchestrationClient]IDurableOrchestrationClient starter,
        ILogger log)
        {
            var context = new DecoratorContext();
            new DecoratorRegistry().GetDecorator().Decorate(context, req);

            // var instanceId = await starter.StartNewAsync(nameof(CreatePRReviewDecorator), cIContext);
            // DurableOrchestrationStatus status = await starter.GetStatusAsync(instanceId, false, false);
            // return (ActionResult)new OkObjectResult(status);
            return (ActionResult)new OkObjectResult("hello");
            ;
        }

        [FunctionName("GitHubPRCommentHook")]
        public async Task<IActionResult> GitHubPRCommentHook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [OrchestrationClient]IDurableOrchestrationClient starter,
            ILogger log)
        {
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //log.LogInformation(requestBody);
            //// Parse the request 
            //var comment = JsonConvert.DeserializeObject<PRCommentCreated>(requestBody);

            //// Start Orchestrator
            //var commandName = comment.CommandName();

            //if (!string.IsNullOrEmpty(commandName))
            //{
            //    string instanceId = await starter.StartNewAsync(nameof(CreateWorkItemCommand), comment);
            //    log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
            //    DurableOrchestrationStatus status = await starter.GetStatusAsync(instanceId, false, false);
            //    return (ActionResult)new OkObjectResult(status);
            //}
            //else
            //{
            //    var status = new DurableOrchestrationStatus();
            //    status.RuntimeStatus = OrchestrationRuntimeStatus.Completed;
            //    return (ActionResult)new OkObjectResult(status);
            //}
            return (ActionResult)new OkObjectResult("hello");
        }
    }
}
