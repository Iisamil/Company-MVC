using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                // Manual Mapping
                if (role is not null)
                {
                    var mappedRole = new RoleViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name
                    };
                    return View(new List<RoleViewModel>() { mappedRole });
                }
                return View(Enumerable.Empty<RoleViewModel>());
            }
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(roleVM);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM);
        }

        #endregion

        #region Details

        // /User/Details/Guid
        //[HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest(); // 400
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound(); // 404

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(viewName, mappedRole);
            // viewName => kda lazm yegele request b Edit 3l4an yrg3 el View bta3 el Edit lw mga4 edit hyrg3 el Default Details

        }

        #endregion

        #region Edit

        // /Role/Edit/Guid
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lly 3awz y3ml edit hy3ml through el website bssssssssssssssssssssssssssssss
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel updateRole)
        {
            if (id != updateRole.Id)
                return BadRequest(); // to prevent anyone to make changes in inspect page in browser

            if (ModelState.IsValid)
            {
                try // To Avoid Error
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = updateRole.RoleName;

                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception 
                    // 2. Show Freindly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updateRole);
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
                var user = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(user);
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
