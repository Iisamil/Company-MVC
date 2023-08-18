using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; } // PK by Convension

        [Required(ErrorMessage = "Code is Required !")]
        public string Code { get; set; } // nvarchar(max) not required in .net 5

        [Required (ErrorMessage = "Name is Required !")]
        [MaxLength(50)]
        public string Name { get; set; }  
        public DateTime DateOfCreation { get; set; }


        // Navigational Property => MANY
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
