﻿using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Kuzgun.Core.Extensions;
using Kuzgun.Core.Utilities.Interceptors.Autofac;
using Kuzgun.Core.Utilities.IoC;
using Kuzgun.Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kuzgun.Core.Aspects.Autofac.Authorization
{
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
           _httpContextAccessor= ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                    
                }
            }
            throw  new System.Exception(AspectMessages.AuthorizationDenied);
        }
    }
}
