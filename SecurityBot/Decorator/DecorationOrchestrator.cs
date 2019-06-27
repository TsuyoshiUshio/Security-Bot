using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Build.Framework;
using SecurityBot.Model;

namespace SecurityBot.Decorator
{
    public class DecorationOrchestrator
    {
        [FunctionName(nameof(DecorationOrchestrator))]
        public async Task Orchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var decoratorContext = context.GetInput<DecoratorContext>();
            // Get issues -> Activity to fetch the issues 
            var issues =
                await context.CallActivityAsync<IEnumerable<Issue>>(nameof(DecorationOrchestrator) + "_GetIssues",
                    decoratorContext);

            // Get state (CreateOrGetEntity)
            // Create Comment with Issue only for not commented yet.
            // Update State
        }

        [FunctionName(nameof(DecorationOrchestrator) + "_GetIssues")]
        public async Task<IEnumerable<Issue>> GetIssuesAsync(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger logger)
        {
            var decorationContext = context.GetInput<DecoratorContext>();
            // Get Decorator
            // Get Issues 
            // Return Value
            return null;
        }

        [FunctionName(nameof(DecorationOrchestrator) + "_CreateComments")]
        public async Task CreateCommentsAsync(
            [ActivityTrigger] IDurableActivityContext context,
            ILogger logger)
        {
            // Create Comment from Issues. 

        }
    }
}
