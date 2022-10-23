using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Reservation
{
    public interface IReservationService
    {
        Task<ReservationEntity> Get(Guid id);
        Task<IEnumerable<ReservationEntity>> GetAll(); 
        Task<IEnumerable<ReservationEntity>> GetAllWithItens();
        Task<ReservationEntity> Post(ReservationEntity item);
        Task<ReservationEntity> Put(ReservationEntity item);
        Task<bool> Delete(Guid id);
    }
}