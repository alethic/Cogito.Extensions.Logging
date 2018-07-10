using Autofac;

using Cogito.Autofac;
using Cogito.Autofac.DependencyInjection;
using Cogito.Extensions.Logging.Serilog.Autofac;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileAndServe.Components.Logging
{

    [RegisterAs(typeof(IServiceCollectionConfigurator))]
    public class ServiceCollectionConfigurator :
        IServiceCollectionConfigurator
    {

        public IServiceCollection Apply(IServiceCollection services)
        {
            services.RemoveWhere(i => i.ImplementationType?.IsAssignableTo<ILoggerFactory>() ?? false);
            services.RemoveWhere(i => i.ImplementationType?.IsAssignableTo<ILoggerProvider>() ?? false);
            return services;
        }

    }

}
