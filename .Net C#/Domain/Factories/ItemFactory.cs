using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data.Abstraction;

namespace Todo.Domain.Factories
{
    public class ItemFactory
    {
        public static Item Create(string description = null, IEnumerable<INoteData> notes = null)
        {
            return new Item
            {
                CreateDate = DateTimeOffset.Now,
                Description = description,
                Notes = notes != null ? notes.Select(NoteFactory.CreateFrom).ToList() : new List<Note>()
            };
        }

        public static Item CreateFrom<T>(IItemData<T> itemData) where T : IEnumerable<INoteData>
        {
            return new Item
            {
                Id = itemData.Id,
                CreateDate = itemData.CreateDate,
                CompleteDate = itemData.CompleteDate,
                ArchiveDate = itemData.ArchiveDate,
                Description = itemData.Description,
                Notes = itemData.Notes.Select(NoteFactory.CreateFrom).ToList()
            };
        }
    }
}