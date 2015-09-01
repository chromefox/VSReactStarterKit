using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentApplication.Classes;

namespace ExperimentApplication.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        public abstract long EntityId { get; }
    }
}
