using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly Session02DbContext _dbContext;
        public GenericRepository(Session02DbContext dbContext) // Ask CLR For Object From DbContext
        {
            //dbContext = /*new Session02DbContext();*/
            _dbContext = dbContext;
        }
        public async Task Add(T item)
            => await _dbContext.Set<T>().AddAsync(item); // ==> Asyncrounous


        public void Delete(T item)
            => _dbContext.Set<T>().Remove(item); // mlha4 Async

        public async Task<T> Get(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee)) // 3l4an azhr el department fe el view bta3 el employee | EagerLoading
                return  (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).ToListAsync();
            else
                return await _dbContext.Set<T>().ToListAsync();
        }



        public void Update(T item)
            => _dbContext.Set<T>().Update(item); // mlha4 Async

    }
}
