using Autofac;

using Cogito.Autofac;
using Cogito.Autofac.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cogito.Extensions.Logging.Serilog.Autofac
{

    /// <summary>
    /// Removes existing logging implementations from the <see cref="IServiceCollection"/>.
    /// </summary>
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
