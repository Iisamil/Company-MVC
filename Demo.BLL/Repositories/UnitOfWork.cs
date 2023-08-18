using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Session02DbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get ; set ; }      // Automatic Property
        public IDepartmentRepository DepartmentRepository { get; set; }    // Automatic Property

        public UnitOfWork(Session02DbContext dbContext) // kda ana hna ask CLR to create object from DbContext w lly byge bb3to l EmployeeRepository
        {
            EmployeeRepository   = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync();
        // 1. hro7 l GenericRepository h4el mn el method kolha SaveChanges()
        // 2. hro7 3nd [Create | Edit | Delete] lly f empController a3ml calling l Func Complete();
        // 3mlt Complete 3l4an ay haga a3mlha tsm3 mra wa7da m4 kol mara yro7 y3ml SaveChanges()


        public void Dispose()
            => _dbContext.Dispose();
        // b3d m l 4o8l y5ls hy3ml Dispose ll Database
        
    }
}
