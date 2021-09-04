using System;
using System.Collections.Generic;

namespace Todo.Domain
{
    public class Item
    {
        /// <summary>
        /// Should be set by the repository
        /// </summary>
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public List<Note> Notes { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? CompleteDate { get; set; }
        public DateTimeOffset? ArchiveDate { get; set; }

        public bool IsCompleted => CompleteDate != null;
        public bool IsArchived => ArchiveDate != null;
    }
}
