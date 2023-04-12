using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineExamination.Models;
using OnlineExamination.Models.DTO;
using OnlineExamination.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace OnlineExamination.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto userLogin)
        {
            if (!ModelState.IsValid) return View();

            var validUser = await _accountService.AuthanticateUser(userLogin);
            if (validUser)
            {
                var userData = await _accountService.GetUserData(userLogin.UserName);

                var claim = new List<Claim>() {
                    new Claim(ClaimTypes.Name,userData.UserName),
                    new Claim(ClaimTypes.Email,userData.Email),
                    new Claim("Department",userData.RoleName),
                    new Claim(userData.RoleName,"true")
                };

                var identity = new ClaimsIdentity(claim, "OnlineExamCookiesAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("OnlineExamCookiesAuth", claimsPrincipal);
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userData));

                if (userData.RoleName == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Jobseeker");
                }

            }
            ViewBag.InvalidUser = "Invalid username or password";
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("OnlineExamCookiesAuth");
            HttpContext.Session.Remove("SessionUser");
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}