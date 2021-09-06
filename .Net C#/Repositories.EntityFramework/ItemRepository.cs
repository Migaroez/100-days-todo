using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Domain;
using Todo.Repositories;
using Todo.Repositories.Filters;

namespace Todo.Repositories.EntityFramework
{
    public class ItemRepository : IItemRepository
    {
        private readonly TodoContext _context;

        public ItemRepository(TodoContext context)
        {
            _context = context;
        }

        public Item Get(Guid id)
        {
            return _context.Items.Include(i => i.Notes).FirstOrDefault(i => i.Id == id)?.ToDomain();
        }

        public IEnumerable<Item> GetFiltered(ItemFilter filter)
        {
            // todo actually implement the filter
            IQueryable<Models.Item> items = _context.Items.Include(i => i.Notes);
            if (filter?.ArchivedOnly == true)
            {
                items = items.Where(i => i.ArchiveDate != null);
            }

            if (filter?.UnArchivedOnly == true)
            {
                items = items.Where(i => i.ArchiveDate == null);
            }

            return items.Select(i => i.ToDomain());
        }

        public Item Upsert(Item item)
        {
            // see if item exists
            var existingItem = _context.Items.Include(i => i.Notes).FirstOrDefault(i => i.Id == item.Id);

            // if not, save and all its notes
            if (existingItem == null)
            {
                var newItem = Models.Item.FromDomain(item);
                _context.Items.Add(newItem);
                _context.SaveChanges();
                return newItem.ToDomain();
            }

            // if exist, update notes list and update items
            existingItem.UpdateFromDomain(item);
            _context.Update(existingItem);
            _context.SaveChanges();
            return existingItem.ToDomain();
        }

        public bool Delete(Guid id)
        {
            // delete if exists
            var existingItem = _context.Items.Include(i => i.Notes).FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return false;
            }

            _context.Remove(existingItem);
            _context.SaveChanges();
            return true;
        }
    }
}
