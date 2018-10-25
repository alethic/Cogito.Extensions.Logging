using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Activators.Reflection;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;

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

            // register root logger
            builder.Register(ctx => ctx.Resolve<ILoggerFactory>().CreateLogger(""))
                .SingleInstance();

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

            builder.RegisterSource(new LoggerRegistrationSource());
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            // ignore components that provide loggers
            if (registration.Services.OfType<TypedService>().Any(ts => ts.ServiceType.IsAssignableTo<ILogger>()))
                return;

            if (registration.Activator is ReflectionActivator ra)
            {
                var parameters = ra.ConstructorFinder
                    .FindConstructors(ra.LimitType)
                    .SelectMany(ctor => ctor.GetParameters());

                if (parameters.Any(pi => pi.ParameterType == typeof(ILogger)))
                {
                    registration.Preparing += (sender, args) =>
                    {
                        var logger = args.Context.Resolve<ILoggerFactory>().CreateLogger(registration.Activator.LimitType);
                        args.Parameters = args.Parameters.Append(TypedParameter.From(logger));
                    };
                }
            }
        }

        class LoggerRegistrationSource :
            IRegistrationSource
        {

            public bool IsAdapterForIndividualComponents => false;

            public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
            {
                if (service is IServiceWithType s)
                {
                    if (s.ServiceType == typeof(ILogger))
                        yield return new ComponentRegistration(
                            Guid.NewGuid(),
                            new DelegateActivator(s.ServiceType, (c, p) => c.Resolve<ILoggerFactory>().CreateLogger("")),
                            new CurrentScopeLifetime(),
                            InstanceSharing.None,
                            InstanceOwnership.OwnedByLifetimeScope,
                            new[] { service },
                            new Dictionary<string, object>());

                    if (s.ServiceType.IsGenericType &&
                        s.ServiceType.GetGenericTypeDefinition() == typeof(ILogger<>))
                        yield return new ComponentRegistration(
                            Guid.NewGuid(),
                            new DelegateActivator(s.ServiceType, (c, p) =>
                                Activator.CreateInstance(
                                    typeof(Logger<>).MakeGenericType(s.ServiceType.GetGenericArguments()[0]),
                                    c.Resolve<ILoggerFactory>())),
                            new CurrentScopeLifetime(),
                            InstanceSharing.None,
                            InstanceOwnership.OwnedByLifetimeScope,
                            new[] { service },
                            new Dictionary<string, object>());
                }
            }

        }

    }

}
