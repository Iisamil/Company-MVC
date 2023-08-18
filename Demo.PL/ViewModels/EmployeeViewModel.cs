using System.ComponentModel.DataAnnotations;
using System;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        // el class da hyb2a viewmodel ll employee 3l4an el annonation m4 bb2a m7tag aktbha hnak





        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Character")]
        [MinLength(5)]
        public string Name { get; set; }

        [Range(22, 30)]
        public int? Age { get; set; }

        // ^ => Start with
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{4,10}-[a-zA-Z]{4,10}$",
            ErrorMessage = "Address Must be Like 123-Street-City-Country")]
        public string Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }    // Save Image

        public string ImageName { get; set; }   // Save ImageName hy7slha mapping

        //[ForeignKey("Department")]
        public int? DepartmentId { get; set; } // ForeignKey => Allow Null => Restrict

        // Navigational Property => ONE
        public Department Department { get; set; }


    }
}
