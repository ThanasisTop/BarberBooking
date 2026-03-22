using BarberBooking.Application.Interfaces;
using BarberBooking.Core.Entities;
using BarberBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        public readonly AppDbContext _context;
        public GenericRepository(AppDbContext context) {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.DateModified = DateTime.UtcNow;

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> where)
        {
            return await _context.Set<T>()
                         .Where(where)
                         .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.DateModified = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
