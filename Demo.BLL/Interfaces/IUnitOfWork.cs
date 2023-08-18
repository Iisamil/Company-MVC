using Demo.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }         // Signature Of Property
        public IDepartmentRepository DepartmentRepository { get; set; }     // Signature Of Property

        Task<int> Complete();
    }
}
