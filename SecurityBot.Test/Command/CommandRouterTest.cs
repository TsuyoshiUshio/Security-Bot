using System;
using System.Collections.Generic;
using System.Text;
using SecurityBot.Command;
using Xunit;

namespace SecurityBot.Test.Command
{
    public class CommandRouterTest
    {
        [Fact]
        public void GetTransitionNormalCase()
        {
            Assert.Equal("falsepositive", CommandRouter.GetTransition(CommandRouter.SuppressFalsePositiveComment));
        }
    }
}
