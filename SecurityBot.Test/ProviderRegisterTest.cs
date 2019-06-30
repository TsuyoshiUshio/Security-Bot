using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SecurityBot.Test
{
    public class ProviderRegisterTest
    {
        [Fact]
        public void RegisterProviderNormalCase()
        {
            var stub = new WebJobsBuilderStub();
            
            var registry = new ProviderRegistry();
            var selectedProvider = new List<string>
            {
                nameof(SampleProviderStartup)
            };
            registry.SelectedStartup = selectedProvider;

            registry.Register(stub);
            var provider = stub.Services.BuildServiceProvider();
            var hello = provider.GetService<IHello>();
            Assert.NotNull(hello);
        }

        public class SampleProviderStartup : IProviderStartup
        {
            public void Configure(IWebJobsBuilder builder)
            {
                builder.Services.AddSingleton<IHello, Hello>();
            }
        }

        public class WebJobsBuilderStub : IWebJobsBuilder
        {
            public IServiceCollection Services { get; }

            public WebJobsBuilderStub()
            {
                Services = new ServiceCollection();
            }
        }

        public interface IHello
        {
            void Say();
        }

        public class Hello : IHello
        {
            public void Say()
            {
                Console.WriteLine("World");
            }
        }

    }
}
