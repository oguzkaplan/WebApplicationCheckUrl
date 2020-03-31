using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationCheckUrl.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            
        }


        public IActionResult Index()
        {
            var getUserList = _userService.GetUserList();
            List<User> users = null;
            if (getUserList.Success)
            {
                users = getUserList.Data;
            }
            return View(users);
        }
    }
}