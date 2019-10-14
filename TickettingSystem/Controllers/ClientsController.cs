﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TickettingSystem.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int? id)
        {
            return View();
        }
    }
}