using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CommandHookContext
    {
        /// <summary>
        /// Command Name
        /// </summary>
        public string CommandName { get; set; }
        /// <summary>
        /// Pull Request Identifier
        /// </summary>
        public Uri PullRequestUri { get; set; }

        /// <summary>
        /// Comment Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ReplyToId is the Id of the Parent Review Comment
        /// </summary>
        public string ReplyToId { get; set; }

        /// <summary>
        /// PullRequestId
        /// </summary>
        public string PullRequestId { get; set; }

        /// <summary>
        /// RepositoryName
        /// e.g. VulnerableApp
        /// </summary>
        public string RepositoryName { get; set; }

        /// <summary>
        /// RepositoryFullName
        /// Includes Owner and RepositoryName eg. TsuyoshiUshio/VulnerableApp
        /// </summary>
        public string RepositoryFullName { get; set; }


    }
}
