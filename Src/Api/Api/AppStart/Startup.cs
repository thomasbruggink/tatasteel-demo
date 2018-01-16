using System;
using System.IO;
using System.Threading;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Api.AppStart
{
    /// <summary>
    ///     Startup configuration for the server
    /// </summary>
    public class Startup
    {
        public static string ServiceName;
        private readonly IHostingEnvironment _hostingEnvironment;

        protected readonly CancellationTokenSource CancellationTokenSource;
        protected IConfigurationRoot Config;
        protected IContainer Container;

        public Startup(IHostingEnvironment env)
        {
            ServiceName = "Api";

            CancellationTokenSource = new CancellationTokenSource();
            _hostingEnvironment = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);

            ConfigureSwagger(services);

            return ConfigureDependencyInjection(services);
        }

        /// <summary>
        ///     Provides the configuration for the application
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            ConfigureSerilog();

            //Setup swagger
            app.UseSwagger();
            //Required to service the swagger ui
            app.UseStaticFiles();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiDemo"); });

            //Since IIS is hosting as a reverse proxy we need to get the original IP from the headers
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            ConfigureJson();

            app.UseMvc();
        }

        private void ConfigureJson()
        {
            var serSettings = new JsonSerializerSettings
            {
                // Default-value ook includen bij serialisatie (let op: DefaultValue-attribuut geeft functionele default aan, niet de technische default)
                DefaultValueHandling = DefaultValueHandling.Include,
                // Indien null, dan afwezig bij serialisatie
                NullValueHandling = NullValueHandling.Ignore,
                // Camel-casing
                ContractResolver = new CamelCasePropertyNamesContractResolver {IgnoreSerializableAttribute = false}
            };
            JsonConvert.DefaultSettings = () => serSettings;
        }

        /// <summary>
        ///     Configures the dependency injection features
        /// </summary>
        protected virtual IServiceProvider ConfigureDependencyInjection(IServiceCollection serviceCollection)
        {
            var containerFactory = new ContainerFactory(serviceCollection);

            containerFactory.CreateContainer();
            Container = containerFactory.Build();

            return new AutofacServiceProvider(Container);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //Create the info block
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ApiDemo"
                });
                //Also include the XML comments
                var xmlPath = Path.Combine(AppContext.BaseDirectory, @"xml/Api.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureSerilog()
        {
            var basePath = AppContext.BaseDirectory + @"/Logs";

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            var configuration = new LoggerConfiguration()
                .Enrich.WithProperty("servicename", ServiceName)
                .Enrich.WithProperty("servername", Environment.MachineName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RollingFile($@"{basePath}/{{Date}}-service.log");

            Log.Logger = configuration.CreateLogger();
        }
    }
}