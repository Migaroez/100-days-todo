using System;
using System.Collections.Generic;
using Domain.Data.Abstraction;

namespace Todo.Domain
{
    public class Item : IItemData<List<Note>>
    {
        /// <summary>
        /// Should be set by the repository when persisted
        /// </summary>
        public Guid? Id { get; internal set; }
        public string Description { get; set; }
        public List<Note> Notes { get; set; }
        public DateTimeOffset CreateDate { get; internal set; }
        public DateTimeOffset? CompleteDate { get; internal set; }
        public DateTimeOffset? ArchiveDate { get; internal set; }

        public bool IsCompleted => CompleteDate != null;
        public bool IsArchived => ArchiveDate != null;

        public void ToggleCompleted()
        {
            CompleteDate = CompleteDate == null ? DateTimeOffset.Now : null;
        }

        public void ToggleArchived()
        {
            ArchiveDate = ArchiveDate == null ? DateTimeOffset.Now : null;
        }
    }
}
