﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeIsShort.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {   
        public IActionResult Index()
        {
            return View();
        }
    }
}
