using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Web.Routing;
using Business.Migrations.MySqlFactories;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Logs";

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            //Configure serilog
            var configuration = new LoggerConfiguration()
                .Enrich.WithProperty("servicename", "DemoApp")
                .Enrich.WithProperty("servername", Environment.MachineName)
                .WriteTo.RollingFile($@"{basePath}\{{Date}}-service.log", LogEventLevel.Debug);

            Log.Logger = configuration.CreateLogger();

            MySqlDatabaseSetup.TrySetupDatabase();
        }
    }
}