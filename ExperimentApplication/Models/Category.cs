using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public long CategoryId { get; set; }

        public string Name { get; set; }
    }
}
