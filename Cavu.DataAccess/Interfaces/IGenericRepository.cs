using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T:class
    {
        IQueryable<T> Entities { get; }
        Task<IEnumerable<T>> All();
        Task<T> GetById(int id);
        
        Task<bool> Add(T entity);
        Task<bool> Delete(int id);
        Task<bool> Upsert(T entity);

    }
}
