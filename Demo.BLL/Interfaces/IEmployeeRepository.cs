using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        #region Info
        // Using IEnumerable | IQueryable => if i want to get records only without Making [Add | Update | Delete]
        // Get All Records => IEnumerable [Parent interface]
        // Filter on Data  => IQueryable
        // Using ICollection => Make [Add | Update | Delete]

        #endregion

        // mynf34 a5leeh 48al Asyncrounous l2no by4t8l fe Database (IQueryable)
        IQueryable<Employee>GetEmployeeByAddress(string address); // His Own Function + 5 Functions which inherit from IGeneric

        // Search
        IQueryable<Employee> SearchEmployeesByName(string name);

    }
}
