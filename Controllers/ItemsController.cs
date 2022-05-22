using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog50.Dtos;
using Catalog50.Entities;
using Catalog50.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog50.Controllers
{
    //GET /items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IInMemItemRepository repository;

        public ItemsController(IInMemItemRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
            .Select(item => item.AsDto()).ToList();
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        //POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existedItem = await repository.GetItemAsync(id);
            if (existedItem is null)
            {
                return NotFound();
            }
            Item updatedItem = existedItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await repository.UpdateItemAsync(updatedItem);
            return NoContent();
        }

        //DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existedItem = await repository.GetItemAsync(id);
            if (existedItem is null)
            {
                return NotFound();
            }
            await repository.DeleteItemAsync(id);
            return NoContent();
        }

    }
}