using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    /// <summary>
    /// Represent Provider agnostic Issue
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Identifier of the Issue 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The type of the Issue
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Message 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Repository path of code that has the issue
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Url For target issue.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Provider of an issue.
        /// e.g. SonarCloud, Aqua 
        /// </summary>
        public string Provider { get; set; }
    }
}
