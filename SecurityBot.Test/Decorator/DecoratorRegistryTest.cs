using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.Services.Common;
using Moq;
using SecurityBot.Decorator;
using SecurityBot.Model;
using Xunit;

namespace SecurityBot.Test.Decorator
{
   
    public class DecoratorRegistryTest
    {

        [Fact]
        public void RegisterDecoratorNormalCase()
        {
            Environment.SetEnvironmentVariable(BotConfiguration.ScannerProviderSetting, "Bar"); // mandatory settings. It is overridden.
            BotConfiguration.CiProvider = "Foo";
            BotConfiguration.ScannerProviders = new List<string>() {"Bar", "Buz"};
            var registry = new DecoratorRegistry();
            var decorator = registry.GetDecorator();
            var context = new DecoratorContext();
            var request = new Mock<HttpRequest>().Object;
            decorator.Decorate(context, request);
            Assert.Equal("3", context.PullRequestId);
            Assert.Equal("BarValue", context.Tag.GetValueOrDefault("Bar"));
            Assert.Equal("BuzValue", context.Tag.GetValueOrDefault("Buz"));

        }

        [Fact]
        public void RegisterScannerDecoratorNormalCase()
        {
            Environment.SetEnvironmentVariable(BotConfiguration.ScannerProviderSetting, "Bar"); // mandatory settings. It is overridden.
            BotConfiguration.ScannerProviders = new List<string>() {"Bar", "Buz"};
            var registry = new DecoratorRegistry();
            var decorator = registry.GetScannerDecorator();
            var context = new DecoratorContext();
            var request = new Mock<HttpRequest>().Object;
            decorator.Decorate(context, request);
            Assert.Equal("BarValue", context.Tag.GetValueOrDefault("Bar"));
            Assert.Equal("BuzValue", context.Tag.GetValueOrDefault("Buz"));
            Assert.Null(context.PullRequestId);
        }

        public class FooDecorator : DecoratorBase
        {
            public override void Update(DecoratorContext context, HttpRequest request)
            {
                context.PullRequestId = "3";
            }
        }
        public class BarDecorator : DecoratorBase
        {
            public override void Update(DecoratorContext context, HttpRequest request)
            {
                context.AddTag("Bar", "BarValue");
            }
        }
        public class BuzDecorator : DecoratorBase
        {
            public override void Update(DecoratorContext context, HttpRequest request)
            {
                context.AddTag("Buz", "BuzValue");
            }
        }
    }


}
