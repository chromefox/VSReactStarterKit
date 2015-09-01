using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
