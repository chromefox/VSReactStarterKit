using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public interface IExperimentContextInterface
    {
        DbSet<User> Users { get; set; }
        DbSet<Listing> Listings { get; set; }
        DbSet<ListingItem> ListingItems { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Gallery> Galleries { get; set; }
    }
}
