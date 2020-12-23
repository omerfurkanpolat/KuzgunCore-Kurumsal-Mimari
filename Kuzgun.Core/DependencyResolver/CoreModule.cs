using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Kuzgun.Core.DependencyResolver
{
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
    }
}
