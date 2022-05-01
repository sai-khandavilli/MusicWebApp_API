using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web;
using Newtonsoft.Json;
using MusicWebApp.Models;

namespace MusicWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoginAsync()
        {
            var admin = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };
            await _userManager.CreateAsync(admin, "Admin@0666");
            return View();
        }

        private async Task<IActionResult> SignInUser(string username, string password)
        {

            var user = await _userManager.FindByEmailAsync(username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    HttpContext.Session.SetString("userLogin", JsonConvert.SerializeObject(user));

                    return Json(new { success = true, 
                                      responseMessage = user, 
                                      redirectUrl = Url.Action("Index", "Home") });

                }

            }

            return Json(new { success = false, responseMessage = "Invalid credentials. Please check your username and password." });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Json(new { success = false, responseMessage = "Email and Password cannot be null." });
            }

            return await SignInUser(email, password);
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]string username, [FromForm]string email, [FromForm]string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return Json(new { success = true, responseMessage = "Account created successfully. Please go back to login" });
                //return Json(new { success = true, responseMessage = Url.Action("Login", "Account") });
            }

            return Json(new { success = false, responseMessage = result.Errors?.First()?.Description });
        }



        public IActionResult Edit()
        {
            var userSession = HttpContext.Session.GetString("userLogin");

            var user = userSession != null ? JsonConvert.DeserializeObject<IdentityUser>(userSession) : null;

            var accountViewModel = new AccountViewModel(user);

            return View("Edit", accountViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] string username, [FromForm] string email)
        {

            var userSession = HttpContext.Session.GetString("userLogin");

            var userSessionData = userSession != null ? JsonConvert.DeserializeObject<IdentityUser>(userSession) : null;

            var user = await _userManager.FindByEmailAsync(userSessionData?.Email);

            // Updating the details
            user.UserName = username;
            user.Email = email;

            IdentityResult result = await _userManager.UpdateAsync(user);


            if (result.Succeeded)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("userLogin", JsonConvert.SerializeObject(user));

                return Json(new { success = true, responseMessage = "Details Updated successfully. Please go back to login" });
                //return Json(new { success = true, responseMessage = Url.Action("Login", "Account") });
            }

            return Json(new { success = false, responseMessage = result.Errors?.First()?.Description });
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Delete()
        {
            var userSession = HttpContext.Session.GetString("userLogin");

            var userSessionData = userSession != null ? JsonConvert.DeserializeObject<IdentityUser>(userSession) : null;

            var user = await _userManager.FindByEmailAsync(userSessionData?.Email);

            await _userManager.DeleteAsync(user);
            
            await _signInManager.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
