using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using SecurityBot.Decorator;

namespace SecurityBot.Command
{
    public class CommandOrchestrator
    {
        [FunctionName(nameof(CommandOrchestrator))]
        public async Task Orchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            // Get the CurrentPullRequestState
            // Execute Command 
            // Update the CurrentPullRequestState
        }

    }   
}
