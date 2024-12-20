using Autofac;

using Cogito.Autofac;

namespace Cogito.Extensions.Logging.Tests
{

    public class AssemblyModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterFromAttributes(typeof(AssemblyModule).Assembly);
        }

    }

}
