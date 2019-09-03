using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TickettingSystem.Controllers
{
    public class TradesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}