using core.extensions;
using entities.entities;
using Microsoft.EntityFrameworkCore;

namespace entities
{
    public class EFApplicationContext : DbContext
    {
        public virtual DbSet<People> Peoples { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public EFApplicationContext(DbContextOptions<EFApplicationContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                var currentTableName = builder.Entity(entity.Name).Metadata.GetTableName();
                builder.Entity(entity.Name).ToTable(currentTableName.ToLower());

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }
        }
    }
}