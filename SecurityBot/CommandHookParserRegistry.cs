using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var name = typeof(CommandHookParserRegistry).Assembly.GetName().Name; // Change this code if you want to search other projects.
            IEnumerable<Assembly> commandParsers2 = AssemblyHelper.GetReferencingAssemblies(name);
                IEnumerable<Type> commandParser3 = commandParsers2.SelectMany(p => p.GetExportedTypes());
                IEnumerable<Type> commandParsers = commandParser3
                .Where(p => p.GetInterfaces().Contains(typeof(ICommandHookParser)));


            foreach (var commandParser in commandParsers)
            {
                var value = nameof(commandParser);

                if (commandParser.Name == (BotConfiguration.RepositoryProvider + "CommandHookParser"))
                {
                    var obj = Activator.CreateInstance(commandParser);
                    builder.Services.AddSingleton<ICommandHookParser>(obj as ICommandHookParser);
                }
            }
        }
    }
}
