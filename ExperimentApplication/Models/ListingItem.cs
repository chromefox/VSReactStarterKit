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
        [Column(Order = 1)]
        public long ListingId { get; set; }

        [ForeignKey("ListingId")]
        
        public virtual Listing Listing { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        [Key]
        [Column(Order = 2)]
        public long ItemId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
