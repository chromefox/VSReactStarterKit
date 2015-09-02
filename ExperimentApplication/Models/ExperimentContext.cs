using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class ExperimentContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<ListingItem> ListingItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DefineUserRelationship(modelBuilder);
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }

        protected void DefineUserRelationship(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasMany(x => x.ChildCategories)
            .WithOptional(x => x.ParentCategory);
        }
    }
}