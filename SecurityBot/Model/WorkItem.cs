using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    /// <summary>
    /// WorkItem that is created.
    /// </summary>
    public class WorkItem
    {
        public string Id { get; set; }
        public string Uri { get; set; }
        public string WorkItemProvider { get; set; }
    }
}
