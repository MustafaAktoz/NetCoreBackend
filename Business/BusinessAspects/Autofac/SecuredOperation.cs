using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Messages;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation:MethodInterception
    {
        string[] _roles;
        IHttpContextAccessor _httpContextAccessor;
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var claimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (claimRoles.Contains(role))
                {
                    return;
                }
            }

            throw new Exception(AspectMessages.AuthorizationDenied);
        }
    }
}
