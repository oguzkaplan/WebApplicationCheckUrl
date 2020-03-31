using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationCheckUrl.Models;

namespace WebApplicationCheckUrl.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private IUrlService _urlService;

        public HomeController(ILogger<HomeController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        public IActionResult Index()
        {
            var result = _urlService.GetUrlListByUserId(SessionUserId);
            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

    }
}
