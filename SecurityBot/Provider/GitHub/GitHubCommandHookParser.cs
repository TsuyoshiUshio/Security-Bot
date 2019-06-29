using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using Newtonsoft.Json;
using SecurityBot.Command;
using SecurityBot.Model;
using SecurityBot.Provider.GitHub.Generated;

namespace SecurityBot.Provider.GitHub
{
    public class GitHubCommandHookParser : ICommandHookParser
    {
        /// <summary>
        /// Create CommandHookContext from the WebHook of a comment.
        /// If the command is not on the list of <see cref="CommandRouter">CommandRouter</see>,
        /// or it is not a comment for pull request, return null
        /// </summary>
        /// <param name="req"></param>
        /// <returns>CommandHookContext object or null</returns>
        public async Task<CommandHookContext> Parse(HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var comment = JsonConvert.DeserializeObject<PRCommentCreated>(requestBody);

            var rawComment = comment?.comment?.body;
            var command = rawComment?.Trim();
            if (CommandRouter.ContainsKey(command) && comment?.pull_request != null)
            {
                return new CommandHookContext()
                {
                    Id = comment.comment?.id.ToString(),
                    CommandName = CommandRouter.GetValueOrDefault(command),
                    PullRequestUri = new Uri(comment.pull_request.html_url),
                    PullRequestId = comment.pull_request.number.ToString(),
                    ReplyToId = comment.comment?.in_reply_to_id.ToString(),
                    RepositoryName = comment.repository.name,
                    RepositoryFullName = comment.repository.full_name
                };
            }
            else
            {
                return null;
            }
            
        }
    }
}
