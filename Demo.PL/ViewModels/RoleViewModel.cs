using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Display(Name = "RoleName")]
        public string RoleName { get; set; }

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
