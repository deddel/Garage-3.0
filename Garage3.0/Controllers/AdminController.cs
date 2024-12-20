﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Data;
using Garage3._0.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Garage3._0.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Garage3._0.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: ApplicationUserViewModels
        public async Task<IActionResult> ControlPanel()
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModels = new List<ApplicationUserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Await each call
                userViewModels.Add(new ApplicationUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FName = user.FName,
                    LName = user.LName,
                    Roles = string.Join(", ", roles)
                });
            }

            return View(userViewModels); // Return a view that lists the users
        }

        public async Task<IActionResult> AdminToggle(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var isAdmin = roles.Contains("Admin");
            IdentityResult result;
            if (isAdmin)
            {
                result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, "Admin");

            }
            if (result.Succeeded)
            {
                //TempData["SuccessMessage"] = isAdmin ? "Admin role removed." : "Admin role added.";
                return RedirectToAction(nameof(ControlPanel));
            }

            return View("Error");
        }

        public async Task<IActionResult> MemberToggle(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var isMember = roles.Contains("Member");
            IdentityResult result;
            if (isMember)
            {
                result = await _userManager.RemoveFromRoleAsync(user, "Member");
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, "Member");

            }
            if (result.Succeeded)
            {
                //TempData["SuccessMessage"] = isMember ? "Member role removed." : "Member role added.";
                return RedirectToAction(nameof(ControlPanel));
            }

            return View("Error");
        }

    }
}
