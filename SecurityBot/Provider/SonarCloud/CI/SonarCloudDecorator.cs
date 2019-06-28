using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SecurityBot.Decorator;
using SecurityBot.Model;

namespace SecurityBot.Provider.SonarCloud.CI
{
    public class SonarCloudDecorator : DecoratorBase
    {
        public override void Update(DecoratorContext context, HttpRequest request)
        {
            string projectKey = request.Query[SonarCloudConfiguration.ProjectKey];
            context.AddTag(SonarCloudConfiguration.ProjectKey, projectKey);
        }
    }
}
