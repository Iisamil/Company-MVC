using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
		public string FName { get; set; }
		public string LName { get; set; }

		[Required(ErrorMessage ="Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid !")]
        public string Email { get; set; }

		[Required(ErrorMessage ="Password Required !")]
		[DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage ="Required !")]
		[Compare("Password",ErrorMessage ="Password Doesn't Match !")]
		[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }












    }
}
