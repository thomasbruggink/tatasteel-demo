using Api.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Api.AppStart
{
    /// <summary>
    ///     Creates a new container containing all the injectable services and repositories
    /// </summary>
    public class ContainerFactory
    {
        private readonly IServiceCollection _serviceCollection;
        protected ContainerBuilder _containerBuilder;

        /// <inheritdoc />
        public ContainerFactory(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        /// <summary>
        ///     Creates a new container
        /// </summary>
        /// <returns></returns>
        public virtual void CreateContainer()
        {
            _containerBuilder = new ContainerBuilder();

            // Populate the API to register all controller
            _containerBuilder.Populate(_serviceCollection);

            // Register the configuration reader
            _containerBuilder.RegisterType<Configuration.Configuration>().AsImplementedInterfaces().SingleInstance();

            // Register services and repositories
            _containerBuilder.RegisterType<ProductFileReader>().AsImplementedInterfaces();
            _containerBuilder.RegisterType<ProductRepository>().AsImplementedInterfaces();
            _containerBuilder.RegisterType<AvalibilityRepository>().AsImplementedInterfaces();
        }

        /// <summary>
        ///     Builds the container
        /// </summary>
        /// <returns></returns>
        public IContainer Build()
        {
            return _containerBuilder.Build();
        }
    }
}