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
            return DependencyContext.Default.RuntimeLibraries.Where(p => IsCandidateLibrary(p, assemblyName)).Select(
                p => Assembly.Load(new AssemblyName(p.Name)));
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == assemblyName || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }
    }
}
