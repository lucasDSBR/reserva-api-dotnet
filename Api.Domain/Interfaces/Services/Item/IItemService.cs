using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Item
{
    public interface IItemService
    {
        Task<ItemEntity> Get(Guid id);
        Task<IEnumerable<ItemEntity>> GetAll();
        Task<ItemEntity> Post(ItemEntity item);
        Task<ItemEntity> Put(ItemEntity item);
        Task<bool> Delete(Guid id);
    }
}