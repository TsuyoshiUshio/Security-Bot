using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyModel;

namespace SecurityBot
{
    /// <summary>
    /// Find implementation of <see cref="IProviderStartup"/> then execute Configure method for adding DI feature foreach providers.
    /// </summary>
    public class ProviderRegistry
    {

        /// <summary>
        /// Select specific Startup class. Mainly for testing purpose.
        /// </summary>
        public List<string> SelectedStartup { get; set; }

        public ProviderRegistry()
        {
            SelectedStartup = new List<string>();
        }
        /// <summary>
        /// Register Providers and invoke Configure method with the builder parameter.
        /// </summary>
        /// <param name="builder">IWebJobsBuilder object.</param>
        public void Register(IWebJobsBuilder builder)
        {
            var name = typeof(ProviderRegistry).Assembly.GetName().Name; // Change this code if you want to search other projects.
            IEnumerable<Type> providerStartup = AssemblyHelper.GetReferencingAssemblies(name).SelectMany(p => p.GetExportedTypes())
                .Where(p => p.GetInterfaces().Contains(typeof(IProviderStartup)));
            // suppress registering 
            IEnumerable<Type> filteredProviderStartup = (SelectedStartup.Count == 0)
                ? providerStartup
                : providerStartup.Where(p => SelectedStartup.Contains(p.Name));

            foreach (var startup in filteredProviderStartup)
            {
                var obj = Activator.CreateInstance(startup);
                var method = startup.GetMethod("Configure");
                method.Invoke(obj, new object[] {builder});
            }
        }
    }
}
