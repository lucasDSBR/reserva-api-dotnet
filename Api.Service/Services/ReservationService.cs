using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Item;
using Api.Domain.Interfaces.Services.Reservation;

namespace Api.Service.Services
{
    public class ReservationService : IReservationService
    {
        private IRepository<ReservationEntity> _repository;
        public ReservationService(IRepository<ReservationEntity> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ReservationEntity> Get(Guid id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<ReservationEntity>> GetAll()
        {
            return await _repository.SelectAsync();
        }

        public async Task<IEnumerable<ReservationEntity>> GetAllWithItens()
        {
            return await _repository.SelectAsyncWithItens();
        }

        public async Task<ReservationEntity> Post(ReservationEntity reservation)
        {

            return await _repository.InsertAsync(reservation);
        }

        public async Task<ReservationEntity> Put(ReservationEntity item)
        {
            return await _repository.UpdateAsync(item);
        }
    }
}