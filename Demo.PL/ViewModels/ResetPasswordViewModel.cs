using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Required !")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Required !")]
        [Compare("NewPassword", ErrorMessage = "Password Doesn't Match !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
