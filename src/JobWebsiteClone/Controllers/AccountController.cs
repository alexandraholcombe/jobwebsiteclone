using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using JobWebsiteClone.Models;
using JobWebsiteClone.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JobWebsiteClone.Controllers
{
    public class AccountController : Controller
    {
        private readonly JobSiteContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        // GET: /<controller>/

        public AccountController(JobSiteContext db, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.User = _db.Users.FirstOrDefault(users => users.UserName == User.Identity.Name);
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult EmployerRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmployerRegister(RegisterViewModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            //            IdentityResult result = userManager.CreateAsync
            //(user, obj.Password).Result;
            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Employer").Result)
                {
                    Role adminRole = new Role();
                    adminRole.Name = "Employer";
                    IdentityResult roleResult = _roleManager
                        .CreateAsync(adminRole).Result;
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("",
                         "Error while creating role!");
                        return View(model);
                    }
                    //_db.Roles.Add(adminRole);
                    //_db.SaveChanges();
                }
                _userManager.AddToRoleAsync(user, "Employer").Wait();
                //var role = await _roleManager.FindByNameAsync("Employer");
                //user.Roles.Add(adminRole);
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Manage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_db.Users.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public IActionResult Delete(string id)
        {
            var thisUser = _db.Users.FirstOrDefault(User => User.Id == id);
            return View(thisUser);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var thisUser = _db.Users.FirstOrDefault(User => User.Id == id);
            _db.Users.Remove(thisUser);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
