using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    /// <summary>
    /// Context object for Decorator.
    /// Usually generated after the PullRequest Triggered CI process.
    /// </summary>
    public class DecoratorContext
    {
        /// <summary>
        /// Pull Request Unique Identifier
        /// </summary>
        public string PullRequestId { get; set; }

        /// <summary>
        /// Pull Request Commit Id
        /// </summary>
        public string CommitId { get; set; }
        /// <summary>
        /// Identifier of a pull request
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Contains Provider Specific Key/Value pair.
        /// </summary>
        public Dictionary<string, string> Tag { get; set; }

        public void AddTag(string key, string value)
        {
            Tag.Add(key, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DecoratorContext()
        {
            Tag = new Dictionary<string, string>();
        }
    }
}
