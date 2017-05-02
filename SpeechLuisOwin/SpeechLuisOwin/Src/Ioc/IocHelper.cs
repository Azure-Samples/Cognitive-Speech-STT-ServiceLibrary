using Microsoft.Extensions.DependencyInjection;
using SpeechLuisOwin.Src.Ioc;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace SpeechLuisOwin.Ioc
{
    public static class IocHelper
    {
        static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
            private set
            {
            }
        }

        /// <summary>
        /// build a provider for IOC 
        /// </summary>
        /// <param name="func"></param>
        public static void BuildServiceProvider(Func<ServiceCollection, ServiceCollection> func = null)
        {
            var services = new ServiceCollection();
            // Register all dependent services
            // 
            // IocSomeAssembly.Register(services);    
            // 
            // services.AddTransient<ISomething, Something>()

            // For WebApi controllers, you may want to have a bit of reflection
            var controllerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
              .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
              .Where(t => typeof(ApiController).IsAssignableFrom(t)
                || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }

            if (func != null)
            {
                services = func(services);
            }

            // It is only that you need to get service provider in the end
            _serviceProvider = services.BuildServiceProvider();
        }
    }
}