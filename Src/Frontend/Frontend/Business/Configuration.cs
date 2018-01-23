using Akka.Configuration;
using System;
using System.IO;

namespace Business
{
    public class MySqlConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Contains all configuration objects from the conf file
    /// </summary>
    public class Configuration
    {
        public string ApiEndpoint { get; set; }
        public MySqlConfig MySqlConfig { get; set; }

        private static Configuration _instance;
        public static Configuration Instance => _instance ?? (_instance = new Configuration());

        public object HostingEnvironment { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"application.conf";

            var content = File.ReadAllText(path);
            var config = ConfigurationFactory.ParseString(content);

            ApiEndpoint = config.GetString("apiEndpoint");
            MySqlConfig = new MySqlConfig
            {
                Server = config.GetString("mysqlServer"),
                Database = config.GetString("mysqlDatabase"),
                UserName = config.GetString("mysqlUser"),
                Password = config.GetString("mysqlPassword")
            };
        }
    }
}
