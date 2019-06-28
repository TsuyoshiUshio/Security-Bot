using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SecurityBot.Model;

namespace SecurityBot.Entity
{
    public class PullRequestEntity
    {
        [FunctionName(nameof(PullRequestEntity))]
        public void EntryPoint(
            [EntityTrigger] IDurableEntityContext ctx)
        {
            var current = ctx.GetState<PullRequestStateContext>();
            var input = ctx.GetInput<PullRequestStateContext>();

            switch (ctx.OperationName)
            {
                case "get":
                    break;
                case "update":
                    current = input;
                    break;
            }
            ctx.SetState(current);
        }

        /// <summary>
        /// Method for getting PullRequest Entity from the Key. 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="client"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName(nameof(PullRequestEntity) + "_GetPullRequestStateContext")]
        public async Task<EntityStateResponse<PullRequestStateContext>> GetPullRequestStateContext(
            [ActivityTrigger] string entityId,
            [OrchestrationClient] IDurableOrchestrationClient client,
            ILogger log
        )
        {
            return await client.ReadEntityStateAsync<PullRequestStateContext>(new EntityId(nameof(PullRequestEntity), entityId));
        }

    }
}
