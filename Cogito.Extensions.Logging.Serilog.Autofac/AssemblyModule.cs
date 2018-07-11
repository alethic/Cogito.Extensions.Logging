using Autofac;

using Cogito.Autofac;

using Microsoft.Extensions.Logging;

using Serilog.Extensions.Logging;

namespace Cogito.Extensions.Logging.Serilog.Autofac
{

    public class AssemblyModule :
        Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterFromAttributes(typeof(AssemblyModule).Assembly);
            builder.Register<ILoggerProvider>(ctx => new SerilogLoggerProvider(ctx.Resolve<global::Serilog.ILogger>(), true)).SingleInstance();
            builder.Register<ILoggerFactory>(ctx => new SerilogLoggerFactory(ctx.Resolve<ILoggerProvider>())).SingleInstance();
        }

    }

}
