using Cavu.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Repositories
{
    public abstract class GenericRepository<T>: IGenericRepository<T> where T: class
    {
        protected ApplicationDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public  IQueryable<T> Entities
        {
            get
            {
                return this.dbSet.AsQueryable<T>();
            }
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
