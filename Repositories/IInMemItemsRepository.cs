using System;
using System.Collections.Generic;
using Catalog50.Entities;

namespace Catalog50.Repositories
{
    public interface IInMemItemRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();

        void CreateItem(Item item);
        void UpdateItem(Item item);
    }
}