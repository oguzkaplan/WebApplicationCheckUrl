using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCheckUrl.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private int _sessionUserId;
        public int SessionUserId
        {
            get
            {
                _sessionUserId = Convert.ToInt32(User.Claims.LastOrDefault().Value);
                return _sessionUserId;
            }

        }
    }
}