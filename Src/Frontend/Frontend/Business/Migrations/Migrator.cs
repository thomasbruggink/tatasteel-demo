using System;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;

namespace Business.Migrations
{
    /// <summary>
    /// Migration class
    /// </summary>
    public static class Migrator
    {
        private class MigrationProcessorOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; set; }
        }

        /// <summary>
        /// Upgrades the database to the latest version
        /// </summary>
        /// <param name="processorFactory"></param>
        /// <param name="connectionString"></param>
        public static void MigrateToLatest(
            MigrationProcessorFactory processorFactory, 
            string connectionString)
        {
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));

            var migrationContext = new RunnerContext(announcer)
            {
                Targets = new[]
                {
                    nameof(Migrator)
                }
            };

            // Since migrations might take long increase the command timeout for this. The timeout is in seconds
            var options = new MigrationProcessorOptions { PreviewOnly = false, Timeout = 60 * 30 }; // 30 minutes

            using (var processor = processorFactory.Create(connectionString, announcer, options))
            {
                var runner = new MigrationRunner(Assembly.GetAssembly(typeof(Migrator)), migrationContext, processor);
                runner.MigrateUp(true);
            }
        }
    }
}
