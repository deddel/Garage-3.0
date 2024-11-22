using System;
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

        public async Task<IActionResult> AdminToggle(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            //var userId = _userManager.GetUserId(User);
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return View(nameof(ControlPanel));
            }

            return View("Error");

        }

        // Action to manage user roles
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                CurrentRoles = userRoles,
                AllRoles = allRoles
            };

            return View(model);
        }

        // Action to assign roles to users
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction("ManageRoles", new { userId = userId });
            }

            return View("Error");
        }
    }
}
