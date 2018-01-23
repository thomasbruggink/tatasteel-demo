using System;
using System.Reflection;
using Autofac;

namespace UITests.Pages
{
    public class NavigationHelper
    {
        private static NavigationHelper _instance;
        private readonly IContainer _container;

        private NavigationHelper()
        {
            var containerBuilder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();

            containerBuilder.RegisterAssemblyTypes(dataAccess)
                .Where(t => typeof (IPage).IsAssignableFrom(t))
                .AsSelf();

            _container = containerBuilder.Build();
        }

        public static T ResolvePage<T>() where T : IPage
        {
            if (_instance == null)
            {
                _instance = new NavigationHelper();
            }
            return _instance._container.Resolve<T>();
        }

        public static T Navigate<T>() where T : IPage, new()
        {
            var instance = new T();
            var baseUrl = new Uri(AssemblyConfiguration.Driver.Url, UriKind.RelativeOrAbsolute);
            var pageUrl = new Uri(baseUrl, instance.GetUrl());

            // Navigate to the specified URL in the application
            AssemblyConfiguration.Driver.Navigate().GoToUrl(pageUrl);

            return ResolvePage<T>();
        }
    }
}
    