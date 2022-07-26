using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataSet;
        private DbSet<ReservationEntity> _dataSetReservations;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
            _dataSetReservations = context.Set<ReservationEntity>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if(result == null)
                    return false;

                _dataSet.Remove(result);

                await _context.SaveChangesAsync();

                return true;

            }       
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if(item.Id == Guid.Empty){
                    item.Id = Guid.NewGuid();
                }

                item.createAt = DateTime.UtcNow;
                _dataSet.Add(item);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return item;
        }
        public async Task<bool> ExistAsync (Guid id)
        {
            return await _dataSet.AnyAsync(P => P.Id.Equals(id));
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ReservationEntity>> SelectAsyncWithItens()
        {
            try
            {
                var query = _dataSetReservations
                    .Include(x => x.Itens)
                    .AsNoTracking();
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                //Realizando a busca de um id no banco  igual ao fornecido pelo front
                //Caso não encontre, insere um default
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if(result == null)
                    return null;
                item.UpdateAt = DateTime.UtcNow;
                item.createAt = result.createAt;


                _context.Entry(result).CurrentValues.SetValues(item);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return item;
        }
    }
}