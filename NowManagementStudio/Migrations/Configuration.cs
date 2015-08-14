namespace NowManagementStudio.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<NowManagementStudio.Models.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            // register mysql code generator
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());

            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(NowManagementStudio.Models.AuthContext context)
        {

        }
    }
}