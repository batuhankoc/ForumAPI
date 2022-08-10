using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            
           return await Task.FromResult(_dbSet.Where(expression));

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.ToList());
           // return await _dbSet.ToListAsync(); bu şekilde de çalışıyor neden fromresult kullandık
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            //_context.Entry(entity).State = EntityState.Deleted;
            await Task.FromResult(_dbSet.Remove(entity));
            await SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_dbSet.Update(entity));
            await SaveChanges();
            //Task.From result kıllanarak async olmayan metodu async hale getiriyoruz
            //await _dbSet.Update(entity);
        }
        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
