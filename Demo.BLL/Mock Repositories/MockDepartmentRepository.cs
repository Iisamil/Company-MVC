using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mock_Repositories
{
    public class MockDepartmentRepository : IDepartmentRepository
    {
        // This Class using for Test DepartmentRepo
        public void Add(Department item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Department item)
        {
            throw new NotImplementedException();
        }

        public Department Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Department item)
        {
            throw new NotImplementedException();
        }

        Task IGenericRepository<Department>.Add(Department item)
        {
            throw new NotImplementedException();
        }

        Task<Department> IGenericRepository<Department>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Department>> IGenericRepository<Department>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
