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

        /// <summary>
        /// AzureDevOpsPat
        /// </summary>
        public static string Pat { get; }

        /// <summary>
        /// AzureDevOps OrganizationURL
        /// </summary>
        public static string OrganizationUrl { get; }

        static AzureDevOpsConfiguration()
        {
            Pat = Environment.GetEnvironmentVariable(AzureDevOpsPatSetting);
            OrganizationUrl = Environment.GetEnvironmentVariable(AzureDevOpsOrganizationUrlSetting);
        }
    }
}
