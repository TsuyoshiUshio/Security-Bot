using System;
using System.Collections.Generic;
using System.Linq;
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
        internal const string ScannerProvidersSetting = "ScannerProviders";
        internal const string CiProviderSetting = "CiProvider";

        /// <summary>
        /// Type of the repository provider. 
        /// GitHub, AzureDevOps etc.
        /// </summary>
        public static string RepositoryProvider { get; internal set; }

        /// <summary>
        /// Type of the WorkItem provider.
        /// AzureDevOps, GitHub etc.
        /// </summary>
        public static string WorkItemProvider { get; internal set; }

        /// <summary>
        /// Provider for CI. Currently AzureDevOps by default.
        /// You don't need to configure this.
        /// </summary>
        public static string CiProvider { get; internal set; }
    

        /// <summary>
        /// type of Scanner Providers
        /// SonarCloud, Aquq etc.
        /// </summary>
        public static IEnumerable<string> ScannerProviders { get; internal set; }


        static BotConfiguration()
        {
            RepositoryProvider = Environment.GetEnvironmentVariable(RepositoryProviderSetting);
            WorkItemProvider = Environment.GetEnvironmentVariable(WorkItemProviderSetting);
            CiProvider = Environment.GetEnvironmentVariable(CiProviderSetting) ?? "AzureDevOps";

            var scannerProviderItems = Environment.GetEnvironmentVariable(ScannerProvidersSetting)?.Split(',');
            if (scannerProviderItems != null)
                ScannerProviders = scannerProviderItems.ToList();
        }
    }
}
