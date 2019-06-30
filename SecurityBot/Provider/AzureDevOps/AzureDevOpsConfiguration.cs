using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Provider.AzureDevOps
{
    /// <summary>
    /// Manage Configuration set by local.settings.json or AppSettings on Function App.
    /// </summary>
    public class AzureDevOpsConfiguration
    {
        internal const string AzureDevOpsPatSetting = "AzureDevOpsPAT";
        internal const string AzureDevOpsOrganizationUrlSetting = "AzureDevOpsOrganizationURL";
        internal const string AzureDevOpsProject = "AzureDevOpsProject";

        /// <summary>
        /// AzureDevOpsPat
        /// </summary>
        public static string Pat { get; }

        /// <summary>
        /// AzureDevOps OrganizationURL
        /// </summary>
        public static string OrganizationUrl { get; }

        /// <summary>
        /// Azure DevOps Project for WorkItem
        /// </summary>
        public static string Project { get; }

        /// <summary>
        /// ProviderName
        /// </summary>
        public const string ProviderName = "AzureDevOps";

        static AzureDevOpsConfiguration()
        {
            Pat = Environment.GetEnvironmentVariable(AzureDevOpsPatSetting);
            OrganizationUrl = Environment.GetEnvironmentVariable(AzureDevOpsOrganizationUrlSetting);
            Project = Environment.GetEnvironmentVariable(AzureDevOpsProject);
        }
    }
}
