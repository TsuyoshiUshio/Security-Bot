using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecurityBot.Model;

namespace SecurityBot
{
    public interface ICommandHookParser
    {
        /// <summary>
        /// ParseAsync Comment Hook HttpRequest to CommandHookContext
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<CommandHookContext> ParseAsync(HttpRequest req);
    }
}
