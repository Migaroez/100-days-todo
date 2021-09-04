using System;
using System.Collections.Generic;
using System.Linq;

namespace Todo.Repositories.Models
{
    public class Item
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? CompleteDate { get; set; }
        public DateTimeOffset? ArchiveDate { get; set; }
        public ICollection<Note> Notes { get; set; }

        public Domain.Item ToDomain()
        {
            return new Domain.Item
            {
                Id = Id,
                Description = Description,
                Notes = Notes?.Select(n => n.ToDomain()).ToList(),
                CreateDate = CreateDate,
                CompleteDate = CompleteDate,
                ArchiveDate = ArchiveDate
            };
        }

        public static Item FromDomain(Domain.Item item)
        {
            return new Item
            {
                Id = item.Id,
                Description = item.Description,
                Notes = item.Notes?.Select(Note.FromDomain).ToList(),
                CreateDate = item.CreateDate,
                CompleteDate = item.CompleteDate,
                ArchiveDate = item.ArchiveDate
            };
        }
    }
}
