﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ExperimentApplication.Models
{
    public class User : BaseEntity
    {
        [Key]
        public long UserId { get; set; }

        public string Name { get; set; }

        public DateTime LastSeen { get; set; }

        public virtual ICollection<Listing> ListingItems { get; set; }
        public override long EntityId => UserId;

        public User(string name)
        {
            Name = name;
        }

        public User()
        {
            
        }

        public bool TryAddListing(Listing listing)
        {
            if (ListingItems == null)
                ListingItems = new List<Listing>();

            if (ListingItems.Count >= 3)
                return false;

            ListingItems.Add(listing);

            return true;
        }
    }
}
