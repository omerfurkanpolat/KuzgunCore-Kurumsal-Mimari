using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Kuzgun.Core.Aspects.Autofac.Exception;
using Kuzgun.Core.CrossCuttingConcerns.Logging.Log4net.Loggers;

namespace Kuzgun.Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttribute = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttribute.AddRange(methodAttributes);
            classAttribute.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));
            classAttribute.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return classAttribute.OrderBy(x => x.Priority).ToArray();


        }
    }
}
