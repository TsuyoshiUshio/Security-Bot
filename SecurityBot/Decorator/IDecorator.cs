using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SecurityBot.Model;

namespace SecurityBot.Decorator
{
    /// <summary>
    /// Decorate the comment when CI finished.
    /// </summary>
    public interface IDecorator
    {
        /// <summary>
        /// Update DecoratorContext by request
        /// </summary>
        /// <param name="context">DecoratorContext</param>
        /// <param name="request">HttpRequest</param>
        void Update(DecoratorContext context, HttpRequest request);

        /// <summary>
        /// Recursively apply Update method. The Context object will be updated.
        /// </summary>
        /// <param name="context">DecoratorContext</param>
        /// <param name="request">HttpRequest</param>
        void Decorate(DecoratorContext context, HttpRequest request);

        /// <summary>
        /// Add decorators
        /// </summary>
        /// <param name="decorator"></param>
        void Add(IDecorator decorator);
    }
}
