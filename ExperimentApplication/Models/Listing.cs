using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class Listing : BaseEntity
    {
        [Key]
        public long ListingId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ListingItem> ListingItems { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public long UserId { get; set; }
    }
}
