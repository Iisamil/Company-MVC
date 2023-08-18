using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}


        #region Register

        // BaseUrl /Account/Register HttpGet
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Register(RegisterViewModel model)
		{
            if (ModelState.IsValid) // Server Side Validation
            {
                var user = new ApplicationUser()
                {
                    FName = model.FName,
                    LName = model.LName,
                    UserName = model.Email.Split('@')[0], // 3l4an ya5od el UserName mn el email
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };

				var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(model);
		}
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = await  _userManager.FindByEmailAsync(model.Email); // Check Email is Correct
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password); // Check Password
                    if (flag)
                    {
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Wrong Password !");
                }
                ModelState.AddModelError(string.Empty, "Email Not Existed !");
            }


			return View(model);
		}

        #endregion

        #region Sign Out

        // new to remove Warning :D
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }





        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); // token valid for user only one time

                    //                                      className   Controller      QueryString                                
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, "https", Request.Scheme);
                    // https://localhost:44366/ControllerName[Account]/ActionName[ResetPassword]?email=hossam@gmail.com&token=554544355dsfds



                    var email = new Email() // this email class in DAL
                    {
                        Subject = "Reset Password",
                        To = user.Email,
                        Body = PasswordResetLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is Not Existed");
            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);

                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
		}
		#endregion
	}
}
