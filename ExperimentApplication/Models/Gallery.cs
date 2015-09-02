using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class Gallery : BaseEntity
    {
        [Key]
        public long GalleryId { get; set; }

        public string ResourceURI { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        public long ItemId { get; set; }

        public override long EntityId => GalleryId;

        public Gallery(string resource)
        {
            ResourceURI = resource;
        }
    }
}
