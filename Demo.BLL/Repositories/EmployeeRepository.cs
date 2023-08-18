using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(Session02DbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            throw new NotImplementedException();
        }

        // Search
        public IQueryable<Employee> SearchEmployeesByName(string name)
            => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

    }
}
