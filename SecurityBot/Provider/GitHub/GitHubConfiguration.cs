using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Provider.GitHub
{
    /// <summary>
    /// Manage Configuration set by local.settings.json or AppSettings on Function App.
    /// </summary>
    class GitHubConfiguration
    {
        internal const string GitHubPatSetting = "GitHubPAT";
        internal const string GitHubOwnerSetting = "GitHubOwner";

        /// <summary>
        /// GitHub Personal Access Token
        /// </summary>
        public static string Pat { get; }

        /// <summary>
        /// GitHub Repository Owner
        /// https://github.com/Owner/Repository
        /// </summary>
        public static string Owner { get; set; }

        static GitHubConfiguration()
        {
            Pat = Environment.GetEnvironmentVariable(GitHubPatSetting);
            Owner = Environment.GetEnvironmentVariable(GitHubOwnerSetting);
        }
    }
}
