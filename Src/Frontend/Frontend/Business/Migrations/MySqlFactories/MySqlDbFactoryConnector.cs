using System.Data.Common;
using FluentMigrator.Runner.Processors;

namespace Business.Migrations.MySqlFactories
{
    /// <summary>
    /// The MySql implementation to the interface IDatabaseConnection
    /// </summary>
    public class MySqlDbFactoryConnector : ReflectionBasedDbFactory
    {
        /// <summary>
        /// Override to use the async version of the MySql driver
        /// </summary>
        public MySqlDbFactoryConnector() : base("MySqlConnector", "MySql.Data.MySqlClient.MySqlClientFactory")
        {
        }

        /// <summary>
        /// Override the createfactory since this factory uses a single instance
        /// </summary>
        /// <returns></returns>
        protected override DbProviderFactory CreateFactory()
        {
            return global::MySql.Data.MySqlClient.MySqlClientFactory.Instance;
        }
    }
}
