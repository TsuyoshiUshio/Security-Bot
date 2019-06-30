using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class GetIssueContext
    {
        public CreatedReviewComment CreatedReviewComment { get; set; }
        public string PullRequestId { get; set; }
    }
}
