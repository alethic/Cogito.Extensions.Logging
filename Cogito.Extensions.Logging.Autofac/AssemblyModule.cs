using System;
using System.Linq;

using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;

using Cogito.Autofac;

using Microsoft.Extensions.Logging;

namespace Cogito.Extensions.Logging.Autofac
{

    /// <summary>
    /// Makes the Microsoft Extensions Logger available to the Autofac container. Implementation expects a registered
    /// <see cref="ILoggerFactory"/>.
    /// </summary>
    public class AssemblyModule :
        Module
    {

        const string TargetTypeParameterName = "Autofac.AutowiringPropertyInjector.InstanceType";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterFromAttributes(typeof(AssemblyModule).Assembly);
            builder.Register(ctx => ctx.Resolve<ILoggerFactory>().CreateLogger(""));

            // register actual provider for logger instances
            builder.Register((c, p) =>
            {
                // find root logger
                var factory = c.Resolve<ILoggerFactory>();

                // generate based on type
                var targetType = p.OfType<NamedParameter>()
                    .FirstOrDefault(np => np.Name == TargetTypeParameterName && np.Value is Type);
                if (targetType != null)
                    return factory.CreateLogger((Type)targetType.Value);

                // default instance
                return factory.CreateLogger("");
            })
            .As<ILogger>()
            .ExternallyOwned();
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // ignore components that provide loggers
            if (registration.Services.OfType<TypedService>().Any(ts => ts.ServiceType == typeof(ILogger)))
                return;

            if (registration.Activator is ReflectionActivator ra)
            {
                // find ctors that accept a logger
                var any = ra.ConstructorFinder
                    .FindConstructors(ra.LimitType)
                    .SelectMany(ctor => ctor.GetParameters())
                    .Any(pi => pi.ParameterType == typeof(ILogger));

                // no ctors found
                if (!any)
                    return;

                // attach event to inject logger
                registration.Preparing += (sender, args) =>
                {
                    // discover context logger instance from registered logger
                    var logger = args.Context.Resolve<ILoggerFactory>()?.CreateLogger(registration.Activator.LimitType);
                    if (logger == null)
                        throw new NullReferenceException();

                    // append logger parameter
                    args.Parameters = new[] { TypedParameter.From(logger) }.Concat(args.Parameters);
                };
            }
        }

    }

}
