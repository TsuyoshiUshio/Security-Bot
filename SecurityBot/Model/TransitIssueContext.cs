using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class TransitIssueContext
    {
        public CreatedReviewComment CreatedReviewComment { get; set; }
        public string Transition { get; set; }

        public Issue Issue { get; set; }
    }
}
