using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;

namespace MvcEntityFrameworkMemory.Models
{
    public class ItemIndexViewModel
    {
        public List<Item> Items { get; set; }
    }
}
