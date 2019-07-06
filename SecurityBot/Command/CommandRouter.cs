using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SecurityBot.Command
{
    public class CommandRouter : Dictionary<string, string>
    {
        public static Dictionary<string, string> Command { get; }

        public new static bool ContainsKey(string key)
        {
            return Command.ContainsKey(key);
        }

        public static string GetValueOrDefault(string key)
        {
            return Command.GetValueOrDefault(key);
        }


        public static string GetTransition(string key)
        {
            return key.Replace("/", "");
        }

        public const string CreateWorkItemComment = "/workitem";
        public const string CreateWorkItemCommand = "CreateWorkItemCommand";
        public const string SuppressFalsePositiveComment = "/falsepositive";
        public const string IssueTransitionCommand = "IssueTransitionCommand";

        static CommandRouter()
        {
            Command = new Dictionary<string, string>()
            {
                { CreateWorkItemComment, CreateWorkItemCommand },
                { SuppressFalsePositiveComment, IssueTransitionCommand }
            };
        }
    }
}
