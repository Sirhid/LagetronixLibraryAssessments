using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data
{
    public class GenericRepo<T> : IGenericRepo<T> where T:class
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _appDbContext
                 .Set<T>()
                 .ToListAsync();
        }
    }
}
