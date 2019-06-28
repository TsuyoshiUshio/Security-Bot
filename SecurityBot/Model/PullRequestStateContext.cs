using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityBot.Model
{
    public class PullRequestStateContext
    {
        public PullRequestStateContext()
        {
            CreatedWorkItem = new HashSet<CreatedWorkItem>(new CreatedWorkItemComparer());
            CreatedReviewComment = new HashSet<CreatedReviewComment>(new CreatedReviewCommentComparer());
        }

        public string PullRequestId { get; set; }

        public HashSet<CreatedWorkItem> CreatedWorkItem { get; set; }
        public HashSet<CreatedReviewComment> CreatedReviewComment { get; set; }

        public Boolean HasCreatedWorkItem(string commentId)
        {
            return (CreatedWorkItem.Count(x => x.CommentId == commentId) == 1);
        }

        public void Add(CreatedWorkItem workItem)
        {
            CreatedWorkItem.Add(workItem);
        }

        public void Add(CreatedReviewComment comment)
        {
            CreatedReviewComment.Add(comment);
        }
    }

    public class CreatedWorkItemComparer : EqualityComparer<CreatedWorkItem>
    {
        public override bool Equals(CreatedWorkItem x, CreatedWorkItem y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            return x.CommentId == y.CommentId;
        }

        public override int GetHashCode(CreatedWorkItem x)
        {
            return x?.CommentId.GetHashCode() ?? 0;
        }
    }

    public class CreatedReviewCommentComparer : EqualityComparer<CreatedReviewComment>
    {
        public override bool Equals(CreatedReviewComment x, CreatedReviewComment y)
        {

            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            return (x.IssueId == y.IssueId);
        }

        public override int GetHashCode(CreatedReviewComment x)
        {
            return x?.IssueId.GetHashCode() ?? 0;
        }
    }
}
