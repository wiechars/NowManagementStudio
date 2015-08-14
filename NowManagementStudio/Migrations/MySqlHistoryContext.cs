using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace NowManagementStudio.Migrations
{
    /// <summary>
    /// Entity Framework Code First uses Migration 
    /// History to keep track of model changes and ensure consistency between the database and conceptual schemas. 
    /// The Migration History table, __migrationhistory, has a primary key that is too large for MySql.
    /// This is part of the fix.
    /// </summary>
    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(DbConnection connection, string defaultSchema)
            : base(connection, defaultSchema)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey).HasMaxLength(200).IsRequired();
        }
    }
}