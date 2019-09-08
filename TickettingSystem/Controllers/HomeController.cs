using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.ApiHelper; 
using TickettingSystem.Models;
using TickettingSystem.Utilities;

namespace TickettingSystem.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            var allClients = ClientsApi.GetAllClients();
            ViewBag.AllClients = allClients.Result;
            return View();
        }

        [Route("clients/{id}")]
        public IActionResult GetClientById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theClient = ClientsApi.GetClientById(id.Value);
            if(theClient == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = theClient });
        }
        [Route("resetpassword/{id}")]
        public async Task<IActionResult> ResetPassword(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            try
            {
                await ClientsApi.ResetPassword(id.Value);
                return Json(new { success = true, msg = "password successfully reset!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, msg = "an error occured!" });
            }
        }

        [HttpPut] 
        [Route("UpdateClient")] 
        public async Task<IActionResult> UpdateClient([FromBody] ClientUpdateViewModel updateModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientsApi.Update(updateModel);
                    return Json(new { success = true, msg = "Data updated sucessfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = "an error occured while updating records!" });
                }
            }
            return Json(new { success = false, msg = "Fill in all required fields" });
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        { 
            return View();
        }

        public IActionResult Contact()
        {
            //ViewData["Message"] = "Your contact page.";


            return View();
        } 

        //[HttpPost]
        //public JsonResult GetData(DataTableParameters dataTableParameters)
        //{
        //    // put a debug here to see the values
        //    // do anything else to handel to posted data
        //    return View();
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
