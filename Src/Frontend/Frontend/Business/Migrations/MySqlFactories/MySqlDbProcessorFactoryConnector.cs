using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Generators.MySql;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.MySql;

namespace Business.Migrations.MySqlFactories
{
    /// <summary>
    /// Creates a new mysql db factory
    /// </summary>
    public class MySqlDbProcessorFactoryConnector : MigrationProcessorFactory
    {
        /// <summary>
        /// Creates a new migrator
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="announcer"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override IMigrationProcessor Create(string connectionString, IAnnouncer announcer, IMigrationProcessorOptions options)
        {
            var factory = new MySqlDbFactoryConnector();
            var connection = factory.CreateConnection(connectionString);
            return new MySqlProcessor(connection, new MySqlGenerator(), announcer, options, factory);
        }
    }
}
