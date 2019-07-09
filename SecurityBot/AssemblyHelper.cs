using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyModel;

namespace SecurityBot
{
    public class AssemblyHelper
    {
        /// <summary>
        /// Get Reference of Assemblies. It also includes the current assembly. 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
             var current = Assembly.GetAssembly(typeof(AssemblyHelper));
             var currentAssemblies = new HashSet<Assembly>() {current};
            var dependencyRuntimeLibraries =  DependencyContext.Default.RuntimeLibraries; // RuntimeLibraries doesn't include current libraries. 
            
            // Launched on Azure Functions Host: selectedRuntimeLibraries are empty. RootCause SecurityBot.Test is not the dependency of SecurityBot.
            // Debug on Test: selectedRuntimeLibraries are SecurityBot and SecurityBot.Test SecurityBot is a dependency of SecurityBot.Test

                var selectedDependencyRuntimeLibraries = dependencyRuntimeLibraries.Where(p => IsCandidateLibrary(p, assemblyName)).Select(
                p => Assembly.Load(new AssemblyName(p.Name)));
             var result = currentAssemblies.Concat(selectedDependencyRuntimeLibraries);
             return result;
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            Console.WriteLine($"{library.Name}: {assemblyName}");
            return library.Name == assemblyName || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }
    }
}
