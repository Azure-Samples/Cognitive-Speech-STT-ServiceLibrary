using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http.Dependencies;

namespace SpeechLuisOwin.Src.Ioc
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider provider;

        public DefaultDependencyResolver(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public object GetService(Type serviceType)
        {
            return provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return provider.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}