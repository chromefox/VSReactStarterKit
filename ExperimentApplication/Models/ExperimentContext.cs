using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class ExperimentContext : DbContext, IExperimentContextInterface
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingItem> ListingItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DefineUserRelationship(modelBuilder);
        }

        protected void DefineUserRelationship(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasMany(x => x.ChildCategories)
            .WithOptional(x => x.ParentCategory);
        }
    }
}