using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class ListingItem : BaseEntity
    {
        [Key]
        public long ListingItemId { get; set; }

        public long ListingId { get; set; }

        [ForeignKey("ListingId")]
        public virtual Listing Listing { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        public long ItemId { get; set; }

        public DateTime DateCreated { get; set; }

        public override long EntityId => ListingItemId;
    }
}
