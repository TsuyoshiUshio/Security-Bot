using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Provider.SonarCloud
{
    /// <summary>
    /// Manage Configuration set by local.settings.json or AppSettings on Function App.
    /// </summary>
    public class SonarCloudConfiguration
    {
        internal const string SonarCloudPatSetting = "SonarCloudPAT";

        /// <summary>
        /// SonarCloudPat
        /// </summary>
        public static string Pat { get; }

        /// <summary>
        /// Provider Name
        /// </summary>
        public const string ProviderName = "SonarCloud";

        /// <summary>
        /// Key of the ProjectKey value of the DecoratorContext's Tag
        /// </summary>
        public const string ProjectKey = "ProjectKey";

        static SonarCloudConfiguration()
        {
            Pat = Environment.GetEnvironmentVariable(SonarCloudPatSetting);
        }
    }
}
