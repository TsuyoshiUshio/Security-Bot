using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CreateIssueTransitionReplyCommentContext
    {
        public string Command { get; set; }
        public Issue Issue { get; set; }

        public string PullRequestId { get; set; }
        public string InReplyTo { get; set; }
    }
}
