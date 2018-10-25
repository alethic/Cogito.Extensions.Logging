using Autofac;

using Cogito.Autofac;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cogito.Extensions.Logging.Tests.Autofac
{

    [TestClass]
    public class ResolutionTests
    {

        [TestMethod]
        public void Can_resolve_logger()
        {
            var b = new ContainerBuilder();
            b.RegisterAllAssemblyModules();
            var c = b.Build();
            c.Resolve<ILogger>().Should().BeAssignableTo<ILogger>();
        }

        [TestMethod]
        public void Can_resolve_generic_logger()
        {
            var b = new ContainerBuilder();
            b.RegisterAllAssemblyModules();
            var c = b.Build();
            c.Resolve<ILogger<ResolutionTests>>().Should().BeAssignableTo<ILogger<ResolutionTests>>();
        }

        [RegisterAs(typeof(NeedsLogger))]
        class NeedsLogger
        {

            public NeedsLogger(ILogger logger)
            {

            }

        }

        [TestMethod]
        public void Can_resolve_object_which_needs_logger()
        {
            var b = new ContainerBuilder();
            b.RegisterAllAssemblyModules();
            var c = b.Build();
            c.Resolve<NeedsLogger>().Should().NotBeNull();
        }

        [RegisterAs(typeof(NeedsGenericLogger))]
        class NeedsGenericLogger
        {

            public NeedsGenericLogger(ILogger<NeedsGenericLogger> logger)
            {

            }

        }

        [TestMethod]
        public void Can_resolve_object_which_needs_generic_logger()
        {
            var b = new ContainerBuilder();
            b.RegisterAllAssemblyModules();
            var c = b.Build();
            c.Resolve<NeedsGenericLogger>().Should().NotBeNull();
        }

    }

}
