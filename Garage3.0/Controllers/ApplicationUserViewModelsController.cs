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

namespace Garage3._0.Controllers
{
    public class ApplicationUserViewModelsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserViewModelsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //private readonly ApplicationDbContext _context;

        //public ApplicationUserViewModelsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        // GET: ApplicationUserViewModels
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users); // Return a view that lists the users
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

        // GET: ApplicationUserViewModels/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var applicationUserViewModel = await _context.ApplicationUserViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (applicationUserViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(applicationUserViewModel);
        //}

        // GET: ApplicationUserViewModels/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ApplicationUserViewModels/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserName,Email,FName,LName,Roles")] ApplicationUserViewModel applicationUserViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(applicationUserViewModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(applicationUserViewModel);
        //}

        //// GET: ApplicationUserViewModels/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var applicationUserViewModel = await _context.ApplicationUserViewModel.FindAsync(id);
        //    if (applicationUserViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(applicationUserViewModel);
        //}

        //// POST: ApplicationUserViewModels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,FName,LName,Roles")] ApplicationUserViewModel applicationUserViewModel)
        //{
        //    if (id != applicationUserViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(applicationUserViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ApplicationUserViewModelExists(applicationUserViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(applicationUserViewModel);
        //}

        //// GET: ApplicationUserViewModels/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var applicationUserViewModel = await _context.ApplicationUserViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (applicationUserViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(applicationUserViewModel);
        //}

        //// POST: ApplicationUserViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var applicationUserViewModel = await _context.ApplicationUserViewModel.FindAsync(id);
        //    if (applicationUserViewModel != null)
        //    {
        //        _context.ApplicationUserViewModel.Remove(applicationUserViewModel);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ApplicationUserViewModelExists(string id)
        //{
        //    return _context.ApplicationUserViewModel.Any(e => e.Id == id);
        //}
    }
}
