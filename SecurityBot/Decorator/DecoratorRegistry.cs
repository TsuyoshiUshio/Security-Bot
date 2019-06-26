using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityBot.Decorator
{
    public class DecoratorRegistry
    {
        public IDecorator GetDecorator()
        {
            var ciProvider = BotConfiguration.CiProvider;
            var scanProviders = BotConfiguration.ScannerProviders;

            var name = typeof(ProviderRegistry).Assembly.GetName().Name; // Change this code if you want to search other projects.
            IEnumerable<Type> decorators = AssemblyHelper.GetReferencingAssemblies(name).SelectMany(p => p.GetExportedTypes())
                .Where(p => p.GetInterfaces().Contains(typeof(IDecorator)));

            IDecorator result = null;
            foreach (var decorator in decorators)
            {
                if (decorator.Name == (ciProvider + "Decorator"))
                {
                    var ciDecorator = (IDecorator)Activator.CreateInstance(decorator);
                    if (result == null)
                    {
                        result = ciDecorator;
                    }
                    else
                    {
                        result.Add(ciDecorator);
                    }
                }

                if (scanProviders.Select(p => p + "Decorator").Contains(decorator.Name))
                {
                    var scanDecorator = (IDecorator) Activator.CreateInstance(decorator);
                    if (result == null)
                    {
                        result = scanDecorator;
                    }
                    else
                    {
                        result.Add(scanDecorator);
                    }
                }

            }

            return result;
        }
    }
}
