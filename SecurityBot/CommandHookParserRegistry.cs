using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace SecurityBot
{
    public class CommandHookParserRegistry
    {
        /// <summary>
        /// CommandHookParserRegistry find implementations of ICommandHookParser which has the name of the RepositoryProvider and register it as DI
        /// </summary>
        /// <param name="builder">IWebJobsBuilder object.</param>
        public void Register(IWebJobsBuilder builder)
        {
            var name = typeof(ProviderRegistry).Assembly.GetName().Name; // Change this code if you want to search other projects.
            IEnumerable<Type> commandParsers = AssemblyHelper.GetReferencingAssemblies(name).SelectMany(p => p.GetExportedTypes())
                .Where(p => p.GetInterfaces().Contains(typeof(ICommandHookParser)));
            foreach (var commandParser in commandParsers)
            {
                if (nameof(commandParser) == (BotConfiguration.RepositoryProvider + "CommandHookParser)"))
                {
                    var obj = Activator.CreateInstance(commandParser);
                    builder.Services.AddSingleton<ICommandHookParser>(obj as ICommandHookParser);
                }
            }
        }
    }
}
