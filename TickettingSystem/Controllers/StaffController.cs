using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.ApiHelper;
using TickettingSystem.Models;

namespace TickettingSystem.Controllers
{
    [Route("staff")]
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //Authenticate user
            // call the api to authenticate 
            string msg = "";
             
            try
            {
                var authData = await StaffApi.Authenticate(model);
                if (authData == null || authData.Token == null)
                {
                    ViewBag.Msg = "Incorrect username oor password";
                    return View(model);
                }
                HttpContext.Session.SetString("JWToken", authData.Token);
                HttpContext.Session.SetString("usrId_", authData.Id.ToString());
            }
            catch (Exception)
            {
                ViewBag.Msg = "An error occured!";
                return View(model);
            }


            return Redirect("~/Home/dashboard");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {

            return Json(new { Data = "Success" });
        }
    }
}