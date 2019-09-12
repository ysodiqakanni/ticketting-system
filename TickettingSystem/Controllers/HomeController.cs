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
        public async Task<IActionResult> Dashboard()
        {
            var allClients = ClientsApi.GetAllClients();
            ViewBag.AllClients = allClients.Result;

            var model = new DashboardViewModel();
            model.Clients = await ClientsApi.SearchClients("");
            model.Trades = await TradesApi.SearchTrades("");
            model.Exchanges = await ExchangeApi.getAllExchange();
            return View(model);
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

        [Route("createNote/{note}")]
        public async Task<IActionResult> createNote(string note)
        {
            if (String.IsNullOrEmpty(note)) throw new ArgumentNullException("Note cannot be null!");
            try
            {
                await ClientsApi.CreateNewNote(note);
                return Json(new { success = true, msg = "Note saved!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, msg = "Error creating note!" });
            }
        }

        [Route("SearchClients/{searchStr}")]
        public async Task<PartialViewResult> SearchClients(string searchStr)
        {
            var model = new DashboardViewModel();
            model.Clients = await ClientsApi.SearchClients(searchStr);
            return PartialView("_ClientListPartial", model);
        }

        [Route("trades/all")]
        public IActionResult GetTrades()
        {
          
            var trades = TradesApi.GetAllTrades();
            if (trades == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = trades });
        }

        [Route("trades/{id}")]
        public IActionResult GetTradeById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theTrade = TradesApi.GetTradeById(id.Value);
            if (theTrade == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = theTrade });
        }

        [Route("exchanges/{id}")]
        public IActionResult GetExchangesById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var exchanges = ExchangeApi.getExchangeByUserId(id.Value);
            if (exchanges == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = exchanges });
        }
        [Route("trades/search")]
        public IActionResult SearchTrade([FromQuery] TradeSearchModel tradeSearch)
        {
            var trades = TradesApi.GetAllTrades();
            if (trades == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = trades });
            //var result=(await TradesApi.GetAllTrades()).Where(trade=>tradeSearch.UserId)
            //return Json(new { success = true, msg = theTrade });
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
