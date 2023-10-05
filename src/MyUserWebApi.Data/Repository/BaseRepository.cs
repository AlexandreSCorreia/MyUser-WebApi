using Microsoft.EntityFrameworkCore;
using MyUserWebApi.Data.Context;
using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Interfaces;

namespace MyUserWebApi.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(AppDbContext context)
        {
            this._context = context;
            this._dataset = _context.Set<T>();
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if(result == null)
                {
                    return false;
                }

                this._dataset.Remove(result);
                await this._context.SaveChangesAsync();

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
                if(item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                }

                item.CreateAt = DateTime.UtcNow;
                this._dataset.Add(item);
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {               
                throw ex;
            }

            return item;
        }

        public Task<T> SelectAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if(result == null)
                {
                    return null;
                }

                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = result.CreateAt;

                this._context.Entry(result).CurrentValues.SetValues(item);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {               
                throw ex;
            }

            return item;
        }
    }
}