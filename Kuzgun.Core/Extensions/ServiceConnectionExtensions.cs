using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Kuzgun.Core.Extensions
{
    public static class ServiceConnectionExtensions
    {
        public static IServiceCollection AddDependecyResolvers(this IServiceCollection services,ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }

            return ServiceTool.Create(services);
        }
    }
}
