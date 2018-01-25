using FluentMigrator;

namespace Business.Migrations
{
    /// <summary>
    ///     ProfileService initial setup
    /// </summary>
    /// <seealso cref="FluentMigrator.Migration" />
    [Migration(1)]
    public class SetupMigration : Migration
    {
        /// <summary>
        ///     Setup the initial tables
        /// </summary>
        public override void Up()
        {
            //GENERIC            
            Create.Table("images")
                .WithColumn("Id").AsString(255).NotNullable().PrimaryKey()
                .WithColumn("ImageBlob").AsCustom("LONGTEXT");
        }

        /// <summary>
        ///     Drops the profilestatus table
        /// </summary>
        public override void Down()
        {
            Delete.Table("images");
        }
    }
}