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
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetFiltered(ItemFilter filter)
        {
            // todo actually implement the filter
            return _context.Items.Include(i => i.Notes).Select(i => i.ToDomain());
        }

        public Item Upsert(Item item)
        {
            // this is just a test
            var newItem = Models.Item.FromDomain(item);
            _context.Items.Add(newItem);
            _context.SaveChanges();
            return newItem.ToDomain();

            // see if item exists

            // if not, save and all its notes

            // if exist, update notes list and update items

            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            // delete if exists

            throw new NotImplementedException();
        }
    }
}
