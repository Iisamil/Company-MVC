using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        #region Without Generic
        //IEnumerable<Department> GetAll();
        //Department Get(int id);
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);

        #endregion
        #region Info
        // Using IEnumerable | IQueryable => if i want to get records only without Making [Add | Update | Delete]
        // Get All Records => IEnumerable [Parent interface]
        // Filter on Data  => IQueryable
        // Using ICollection => Make [Add | Update | Delete]

        #endregion



    }
}
