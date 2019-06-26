using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using SecurityBot.Model;

namespace SecurityBot.Decorator
{
    public abstract class DecoratorBase : IDecorator
    {
        private List<IDecorator> decorators = new List<IDecorator>();
        public abstract void Update(DecoratorContext context, HttpRequest request);

        public void Decorate(DecoratorContext context, HttpRequest request)
        {
            Update(context, request);
            foreach (var decorator in decorators)
            {
                decorator.Decorate(context, request);
            }
        }

        public void Add(IDecorator decorator)
        {
            decorators.Add(decorator);
        }
    }
}
