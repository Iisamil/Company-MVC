using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class Session02DbContext : IdentityDbContext<ApplicationUser>
    {
        public Session02DbContext(DbContextOptions<Session02DbContext> options) : base(options)
        {
            // ay 7d 3awz yklm el db hyklm el ctor da w lazm yb3t haga mn DbContexOptions
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Server = . ; Database = SessionTwoMVC ; Trusted_Connection = true ; MultipleActiveResultSets = true");
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }

        //public DbSet<IdentityRole> Roles { get; set; }


    }
}
