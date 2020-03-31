using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCheckUrl.Areas.Login.Models;

namespace WebApplicationCheckUrl.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LoginViewModel loginModel = new LoginViewModel();
                return View(loginModel);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            bool rtnStatus = false;
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserLogin(model.Email, model.Password);
                if (user.Success && user.Data != null)
                {

                    var userClaims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Email,user.Data.MailAddress),
                    new Claim(ClaimTypes.NameIdentifier,user.Data.Id.ToString())
                    };

                    var identity = new ClaimsIdentity(userClaims,"User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { identity });
                    await HttpContext.SignInAsync(userPrincipal);


                    //var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()));
                    //identity.AddClaim(new Claim(ClaimTypes.Name, user.Data.MailAddress.ToString()));
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));                   
                    rtnStatus = true;
                }
            }

            if (!rtnStatus)
            {
                ModelState.AddModelError("Password", "Invalid login attempt.");

                return View("Index", model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
     
    }
}