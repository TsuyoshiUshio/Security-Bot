using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SecurityBot.Test
{
    public class UriExtensionsTest
    {
        [Fact]
        public void GetEntityId()
        {
            var uri = new Uri("https://github.com/foo/bar/pull/3");
            Assert.Equal("//github.com/foo/bar/pull/3", uri.GetEntityId());
        }
    }
}
