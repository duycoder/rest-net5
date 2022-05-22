using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog50.Entities;

namespace Catalog50.Repositories
{
    public interface IInMemItemRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();

        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}