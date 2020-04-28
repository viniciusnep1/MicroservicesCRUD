using entities.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace entities
{
    public class EFApplicationContext : DbContext
    {
        public EFApplicationContext(DbContextOptions<EFApplicationContext> options)
        : base(options)
        { }

        public virtual DbSet<CatalogBrand> CatalogBrandSet { get; set; }
        public virtual DbSet<CatalogItem> CatalogItemSet { get; set; }
        public virtual DbSet<CatalogType> CatalogTypeSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CatalogItem>().HasKey(c => c.Id);
        }

    }

 
}
