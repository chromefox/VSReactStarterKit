using System;
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

    }
}
