using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CreateWorkItemContext
    {
        public Issue Issue { get; set; }
        public string PullRequestId { get; set; }

        public CreatedReviewComment CreatedReviewComment { get; set; }
    }
}
