﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Kuzgun.Core.CrossCuttingConcerns.Logging;
using Kuzgun.Core.CrossCuttingConcerns.Logging.Log4net;
using Kuzgun.Core.Utilities.Interceptors.Autofac;
using Kuzgun.Core.Utilities.Messages;

namespace Kuzgun.Core.Aspects.Autofac.Logging
{
    public class LogAspect:MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerServiceBase))
            {
                throw  new System.Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase =(LoggerServiceBase) Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(getLogDetail(invocation));
        }

        private LogDetail getLogDetail(IInvocation invocation)
        {
            var logParameters=new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name

                });
            }
           
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetail;
        }
    }
}
