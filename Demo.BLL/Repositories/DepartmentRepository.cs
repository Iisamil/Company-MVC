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
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        #region Without Generic
        //private readonly Session02DbContext _dbContext;
        //public DepartmentRepository(Session02DbContext dbContext) // Ask CLR For Object From DbContext
        //{
        //    //dbContext = /*new Session02DbContext();*/
        //    _dbContext = dbContext;
        //}
        //public int Add(Department department)
        //{
        //    _dbContext.Departments.Add(department);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _dbContext.Departments.Remove(department);
        //    return _dbContext.SaveChanges();
        //}

        //public Department Get(int id)
        //    => _dbContext.Departments.Find(id);

        //public IEnumerable<Department> GetAll()
        //    => _dbContext.Departments.ToList();

        //public int Update(Department department)
        //{
        //    _dbContext.Departments.Update(department);
        //    return _dbContext.SaveChanges();
        //} 
        #endregion

        public DepartmentRepository(Session02DbContext dbContext):base(dbContext)
        {

        }
    }
}
