using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEntityFrameworkMemory.Models
{
    public class NoteSubmitModel
    {
        public Guid ItemId { get; set; }
        public Guid? Id { get; set; }
        public string Content { get; set; }
    }
}
