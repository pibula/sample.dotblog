﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Mvc5Net48_1
{
    internal class DefaultDependencyResolver : IDependencyResolver
    {
        private ServiceProvider _serviceProvider;

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider as ServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            if (HttpContext.Current?.Items[typeof(IServiceScope)] is IServiceScope scope)
            {
                return scope.ServiceProvider.GetService(serviceType);
            }

            return this._serviceProvider.GetService(serviceType);
            throw new InvalidOperationException("IServiceScope not provided");
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (HttpContext.Current?.Items[typeof(IServiceScope)] is IServiceScope scope)
            {
                return scope.ServiceProvider.GetServices(serviceType);
            }

            return this._serviceProvider.GetServices(serviceType);
            throw new InvalidOperationException("IServiceScope not provided");
        }

        //public IDependencyScope BeginScope()
        //{
        //    return new DefaultDependencyResolver(this.ServiceProvider.CreateScope().ServiceProvider);
        //}

        public void Dispose()
        {
            this._serviceProvider?.Dispose();
            //throw new NotImplementedException();
        }
    }
}