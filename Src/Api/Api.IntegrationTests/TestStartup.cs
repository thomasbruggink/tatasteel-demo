using System;
using Api.AppStart;
using Api.IntegrationTests.Mocks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTests
{
    internal class ContainerFactoryMock : ContainerFactory
    {
        public ContainerFactoryMock(IServiceCollection serviceCollection) : base(serviceCollection)
        {
        }

        public override void CreateContainer()
        {
            base.CreateContainer();
            // Overwrite existing services with mocks
            _containerBuilder.RegisterType<ConfigurationMock>().AsImplementedInterfaces();
            _containerBuilder.RegisterType<ProductFileReaderMock>().AsImplementedInterfaces();
        }
    }


    /// <summary>
    ///     In this class we inherit from the default Startup.cs and then proceed to override/overwrite certain classes and
    ///     substitute them with test/mock classes.
    /// </summary>
    /// <seealso cref="Startup" />
    internal class TestStartup : Startup
    {
        public TestStartup() : base(new HostingEnvironment())
        {
        }

        /// <summary>
        ///     Overrides the dependency injection so that it includes test types
        /// </summary>
        /// <param name="collection"></param>
        protected override IServiceProvider ConfigureDependencyInjection(IServiceCollection collection)
        {
            var containerFactory = new ContainerFactoryMock(collection);
            containerFactory.CreateContainer();
            Container = containerFactory.Build();
            TestInitialize.Container = Container;

            return new AutofacServiceProvider(Container);
        }
    }
}