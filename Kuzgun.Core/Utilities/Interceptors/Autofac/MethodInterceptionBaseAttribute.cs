﻿using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Interceptor;

namespace Kuzgun.Core.Utilities.Interceptors.Autofac
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute:Attribute, IInterceptor
    {
        public int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {
            
        }
    }
}
