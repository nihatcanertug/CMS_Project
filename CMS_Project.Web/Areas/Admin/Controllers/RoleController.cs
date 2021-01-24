﻿using CMS_Project.Entity.Entities.Concrete;
using CMS_Project.Web.Areas.Admin.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Project.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        public IActionResult Index() => View(_roleManager.Roles);
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([MinLength(2, ErrorMessage = "Minimum length is 2"), Required(ErrorMessage = "Must to into role name")] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult ıdentityResult = await _roleManager.CreateAsync(new IdentityRole(name));
                if (ıdentityResult.Succeeded)
                {
                    TempData["Success"] = "The role has been created..!";
                    return RedirectToAction("Index");
                }
                else  foreach (IdentityError error in ıdentityResult.Errors) ModelState.AddModelError("", error.Description);           
            }                 
                TempData["Error"] = "The role hasn't been created..!";
                return View(name);          
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);
            }

            return View(new RoleEdit { Role = role, HasRole = hasRole, HasNotRole = hasNotRole });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleEdit roleEdit)
        {
            IdentityResult result;

            foreach (var userId in roleEdit.AddIds)
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(appUser, roleEdit.RoleName);
            }

            foreach (var userId in roleEdit.DeleteIds)
            {
                AppUser appUser = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(appUser, roleEdit.RoleName);
            }

            return RedirectToAction("Index");
        }
    }
}
