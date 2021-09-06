using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFrameworkMemory.Models
{
    public class ItemSubmitModel
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
    }
}
