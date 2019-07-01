using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBot
{
    public static class UriExtensions
    {
        /// <summary>
        /// Get the Durable Entity's key from URL.
        /// If the URI is https://github.com/MicrosoftDocs/azure-docs-pr/pull/76275
        /// the EntityId will be //github.com/MicrosoftDocs/azure-docs-pr/pull/76275
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetEntityId(this Uri uri)
        {
            var path = uri.AbsoluteUri;
            var a =  path.Split(':');
            return a[1].Replace("/", "_");
        }
    }
}
