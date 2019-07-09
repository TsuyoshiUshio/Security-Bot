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
        /// GetReference
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetReferencingAssemblies(string assemblyName)
        {
           //  var current = Assembly.GetAssembly(typeof(AssemblyHelper));
           //  var result = new List<Assembly>() {current};
            var runtimeLibraries =  DependencyContext.Default.RuntimeLibraries.Where(p => IsCandidateLibrary(p, assemblyName)).Select(
                p => Assembly.Load(new AssemblyName(p.Name)));
            // result.Concat(runtimeLibraries);
            return runtimeLibraries;
            // return result;
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            Console.WriteLine($"{library.Name}: {assemblyName}");
            return library.Name == assemblyName || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }
    }
}
