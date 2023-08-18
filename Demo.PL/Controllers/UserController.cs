using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }

		#region Index
		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
				// Manual Mapping
				var mappedUser = new UserViewModel
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { mappedUser });
			}
		}
        #endregion

        #region Details

        // /User/Details/Guid
        //[HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details") 
        {
            if (id == null)
                return BadRequest(); // 400
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(); // 404

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mappedUser);
            // viewName => kda lazm yegele request b Edit 3l4an yrg3 el View bta3 el Edit lw mga4 edit hyrg3 el Default Details

        }

        #endregion

        #region Edit

        // /User/Edit/Guid
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lly 3awz y3ml edit hy3ml through el website bssssssssssssssssssssssssssssss
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel updateUser)
        {
            if (id != updateUser.Id)
                return BadRequest(); // to prevent anyone to make changes in inspect page in browser

            if (ModelState.IsValid)
            {
                try // To Avoid Error
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FName = updateUser.FName;
                    user.LName = updateUser.LName;
                    user.PhoneNumber = updateUser.PhoneNumber;
                    user.Email = updateUser.Email;                  // To Change Email
                    user.SecurityStamp = Guid.NewGuid().ToString(); // To Change Email


                    await _userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception 
                    // 2. Show Freindly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updateUser);
        }
        #endregion

        #region Delete

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Freindly Msg

                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion


        #endregion







    }
}
