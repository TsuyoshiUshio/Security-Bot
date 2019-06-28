using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Model
{
    public class CreatedReviewComment
    {
        public string CommentId { get; set; }
        public string IssueId { get; set; }
        /// <summary>
        /// Provider specific column.
        /// Currently copying from DecoratorContext.Tag
        /// </summary>
        public Dictionary<string, string> Tag { get; set; }

        public CreatedReviewComment()
        {
            Tag = new Dictionary<string, string>();
        }
    }
}
