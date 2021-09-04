using System;
using System.Collections.Generic;
using System.ComponentModel;
using Todo.Repositories.Filters;

namespace Todo.Repositories
{
    public interface IItemRepository
    {
        public Domain.Item Get(Guid id);
        public IEnumerable<Domain.Item> GetFiltered(ItemFilter filter);
        public Domain.Item Upsert(Domain.Item item);

        /// <summary>
        /// Permanently deletes an Item from storage including all of its linked notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id);
    }
}
