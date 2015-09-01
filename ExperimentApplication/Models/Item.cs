using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
