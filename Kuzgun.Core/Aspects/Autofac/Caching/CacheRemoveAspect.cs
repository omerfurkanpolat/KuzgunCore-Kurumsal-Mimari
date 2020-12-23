using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Kuzgun.Core.CrossCuttingConcerns.Caching;
using Kuzgun.Core.Utilities.Interceptors.Autofac;
using Kuzgun.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Kuzgun.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();

            
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
