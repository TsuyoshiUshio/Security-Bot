using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CreateWorkItemReplyCommentContext
    {
        public string PullRequestId { get; set; }
        public WorkItem WorkItem { get; set; }
        public string InReplyTo { get; set; }
    }
}
