using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Repositories.Filters
{
    public class ItemFilter
    {
        public bool UnArchivedOnly { get; set; }
        public bool ArchivedOnly { get; set; }
        public Guid? NoteId { get; set; }
    }
}
