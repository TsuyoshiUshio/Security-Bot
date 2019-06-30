using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CreateSimpleReplyCommentContext
    {
        public string PullRequestId { get; set; }
        public string Body { get; set; }
        public string InReplyTo { get; set; }
    }
}
