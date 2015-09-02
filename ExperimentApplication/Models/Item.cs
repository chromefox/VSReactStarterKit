using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class Item: BaseEntity
    {
        [Key]
        public long ItemId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<ListingItem> ListingItems { get; set; } 

        public virtual  ICollection<Gallery> Galleries { get; set; } 

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public long? CategoryId { get; set; }

        public override long EntityId => ItemId;

        public Item(string title, Category category)
        {
            Title = title;
            Category = category;
            CreatedDateTime = DateTime.UtcNow;
        }
    }
}
