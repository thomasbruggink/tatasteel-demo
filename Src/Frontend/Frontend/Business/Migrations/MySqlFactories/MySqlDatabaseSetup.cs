using MySql.Data.MySqlClient;

namespace Business.Migrations.MySqlFactories
{
    public static class MySqlDatabaseSetup
    {
        /// <summary>
        /// Try to Create and Migrate database.
        /// </summary>
        public static void TrySetupDatabase()
        {
            var connectionString = CreateConnectionString();

            Migrator.MigrateToLatest(
                new MySqlDbProcessorFactoryConnector(),
                connectionString);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static string CreateConnectionString()
        {
            var config = Configuration.Instance.MySqlConfig;
            return new MySqlConnectionStringBuilder
            {
                Database = config.Database,
                Server = config.Server,
                UserID = config.UserName,
                Password = config.Password,
                CharacterSet = "utf8"
            }.ConnectionString;
        }
    }
}
