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
        /// Register Providers and invoke Configure method with the builder parameter.
        /// </summary>
        /// <param name="builder">IWebJobsBuilder object.</param>
        public void Register(IWebJobsBuilder builder)
        {
            var name = typeof(ProviderRegistry).Assembly.GetName().Name; // Change this code if you want to search other projects.
            IEnumerable<Type> providerStartup = GetReferencingAssemblies(name).SelectMany(p => p.GetExportedTypes())
                .Where(p => p.GetInterfaces().Contains(typeof(IProviderStartup)));
            foreach (var startup in providerStartup)
            {
                var obj = Activator.CreateInstance(startup);
                var method = startup.GetMethod("Configure");
                method.Invoke(obj, new object[] {builder});
            }
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
            return DependencyContext.Default.RuntimeLibraries.Where(p => IsCandidateLibrary(p, assemblyName)).Select(
                p => Assembly.Load(new AssemblyName(p.Name)));
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == assemblyName || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }


    }
}
