using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Item;

namespace Api.Service.Services
{
    public class ItemService : IItemService
    {
        private IRepository<ItemEntity> _repository;
        public ItemService(IRepository<ItemEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ItemEntity> Get(Guid id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<ItemEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<ItemEntity> Post(ItemEntity item)
        {
            return await _repository.InsertAsync(item);
        }

        public async Task<ItemEntity> Put(ItemEntity item)
        {
            return await _repository.UpdateAsync(item);
        }
    }
}