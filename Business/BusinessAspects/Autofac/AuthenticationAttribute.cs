﻿using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class AuthenticationAttribute : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AuthenticationAttribute()
        {
            Priority = 1;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated) throw new UnauthorizedException();
        }
    }
}
