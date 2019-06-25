using System;
using System.Collections.Generic;
using System.Text;
using Environment = System.Environment;

namespace SecurityBot
{
    /// <summary>
    /// Manage Configuration set by local.settings.json or AppSettings on Function App.
    /// </summary>
    public class BotConfiguration
    {
        internal const string RepositoryProviderSetting = "RepositoryProvider";
        internal const string WorkItemProviderSetting = "WorkItemProvider";
        internal const string ScannerProviderSetting = "ScannerProvider";

        /// <summary>
        /// Type of the repository provider. 
        /// GitHub, AzureDevOps etc.
        /// </summary>
        public static string RepositoryProvider { get; }

        /// <summary>
        /// Type of the WorkItem provider.
        /// AzureDevOps, GitHub etc.
        /// </summary>
        public static string WorkItemProvider { get; }

        /// <summary>
        /// type of Scanner Providers
        /// SonarCloud, Aquq etc.
        /// </summary>
        public static IEnumerable<string> ScannerProviders { get;  }


        static BotConfiguration()
        {
            RepositoryProvider = Environment.GetEnvironmentVariable(RepositoryProviderSetting);
            WorkItemProvider = Environment.GetEnvironmentVariable(WorkItemProviderSetting);

            var scannerProviders = new List<string>();
            var scannerProviderItems = Environment.GetEnvironmentVariable(ScannerProviderSetting)?.Split(',');
            if (scannerProviderItems == null)
            {
                throw new ArgumentException(
                    $"ScannerProvider can not be null. Please double check local.settings.json or AppSettings");
            }
            foreach (var provider in scannerProviderItems)
            {
                scannerProviders.Add(provider);
            }

            ScannerProviders = scannerProviders;
        }
    }
}
