using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SecurityBot.Command;
using SecurityBot.Decorator;
using SecurityBot.Model;

namespace SecurityBot
{
    public class ServiceEndpoint
    {

        private ICommandHookParser _commandHookParser;

        public ServiceEndpoint(ICommandHookParser commandHookParser)
        {
            _commandHookParser = commandHookParser;
        }
        [FunctionName("CIHook")]
        public async Task<IActionResult> CIHook(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        [OrchestrationClient]IDurableOrchestrationClient starter,
        ILogger log)
        {
            var context = new DecoratorContext();
            new DecoratorRegistry().GetDecorator().Decorate(context, req);

            var instanceId = await starter.StartNewAsync(nameof(DecorationOrchestrator), context);
            DurableOrchestrationStatus status = await starter.GetStatusAsync(instanceId, false, false);
            return (ActionResult)new OkObjectResult(status);
        }

        [FunctionName("GitHubPRCommentHook")]
        public async Task<IActionResult> GitHubPRCommentHook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req,
            [OrchestrationClient]IDurableOrchestrationClient starter,
            ILogger log)
        {
            var commandContext = await _commandHookParser.ParseAsync(req);
            if (commandContext != null)
            {
                await starter.StartNewAsync(nameof(CommandOrchestrator), commandContext);
            }
            return (ActionResult)new OkObjectResult("Done");
        }
    }
}
