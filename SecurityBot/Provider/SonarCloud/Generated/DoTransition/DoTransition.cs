using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot.Provider.SonarCloud.Generated.DoTransition
{

    public class DoTransition
    {
        public Issue issue { get; set; }
        public Component[] components { get; set; }
        public Rule[] rules { get; set; }
        public object[] users { get; set; }
    }

    public class Issue
    {
        public string key { get; set; }
        public string rule { get; set; }
        public string severity { get; set; }
        public string component { get; set; }
        public string project { get; set; }
        public object[] flows { get; set; }
        public string resolution { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string effort { get; set; }
        public string debt { get; set; }
        public string author { get; set; }
        public string[] tags { get; set; }
        public string[] transitions { get; set; }
        public string[] actions { get; set; }
        public object[] comments { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updateDate { get; set; }
        public string type { get; set; }
        public string organization { get; set; }
        public string pullRequest { get; set; }
        public bool fromHotspot { get; set; }
    }

    public class Component
    {
        public string organization { get; set; }
        public string key { get; set; }
        public string uuid { get; set; }
        public bool enabled { get; set; }
        public string qualifier { get; set; }
        public string name { get; set; }
        public string longName { get; set; }
        public string pullRequest { get; set; }
    }

    public class Rule
    {
        public string key { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
        public string status { get; set; }
        public string langName { get; set; }
    }

}
