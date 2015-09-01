using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
