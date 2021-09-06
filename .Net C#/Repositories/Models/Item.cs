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
                // We do not copy the Id
                Description = item.Description,
                Notes = item.Notes?.Select(Note.FromDomain).ToList(),
                CreateDate = item.CreateDate,
                CompleteDate = item.CompleteDate,
                ArchiveDate = item.ArchiveDate
            };
        }

        public Item UpdateFromDomain(Domain.Item item)
        {
            Description = item.Description;
            CreateDate = item.CreateDate;
            CompleteDate = item.CompleteDate;
            ArchiveDate = item.ArchiveDate;

            // remove notes that no longer exist on the domain model
            foreach ( var deletedNote in Notes.Where(n => item.Notes.Any(note => note.Id == n.Id) == false))
            {
                Notes.Remove(deletedNote);
            }

            // update those that do
            foreach (var note in Notes)
            {
                note.UpdateFromDomain(item.Notes.First(n => n.Id == note.Id));
            }

            // add new ones
            foreach (var newNote in item.Notes.Where(n => Notes.Any(note => note.Id == n.Id) == false))
            {
                Notes.Add(Models.Note.FromDomain(newNote));
            }

            return this;
        }
    }
}
