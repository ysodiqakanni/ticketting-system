using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TickettingSystem.ApiHelper;
using TickettingSystem.DTOs;
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
            model.Trades = await TradesApi.GetAllTrades("");

            model.Notes = await ClientsApi.GetAllNotes();
            model.MembershipData = new Membership
            {
                PackagesAvailable = new List<MembershipAvailablePackagesViewModel>(),
                PackagesPurchased = new List<MembershipPackagesPurchasedViewModel>()
            };
            model.StaffList = await StaffApi.GetAllStaff();
            ViewBag.AllStaff = model.StaffList;
            model.NotesForTheSelectedStaff = new List<StaffNoteViewModel>() { }; // since no staff has been selected yet
            // initially, no client is selected!
            // so search results (D) should contain each of the known exchanges 
            model.Exchanges = await ExchangeApi.GetAllKnownExchanges();
            model.Tickets = await TicketsApi.GetLastTenTickets();
            model.TicketConversations = new List<TicketConversationViewModel>();
            model.NotesForSelectedTicketClient = new List<NoteListViewModel>();

            return View(model);
        }

        [Route("clients/{id}")]
        public IActionResult GetClientById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theClient = ClientsApi.GetClientById(id.Value);
            if (theClient == null) return Json(new { success = false, msg = "record not found!" });
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

        [Route("SearchClients/{searchStr?}")]
        public async Task<PartialViewResult> SearchClients(string searchStr = "")
        {
            var model = new DashboardViewModel();
            model.Clients = await ClientsApi.SearchClients(searchStr);
            return PartialView("_ClientListPartial", model);
        }

        [Route("notes")]
        public async Task<PartialViewResult> Notes()
        {
            var model = new DashboardViewModel();
            model.Notes = await ClientsApi.GetAllNotes();
            return PartialView("_NotesPartial", model);
        }

        //[Route("trades/all")]
        //public IActionResult GetTrades()
        //{

        //    var trades = TradesApi.GetAllTrades();
        //    if (trades == null) return Json(new { success = false, msg = "record not found!" });
        //    return Json(new { success = true, msg = trades });
        //}

        [Route("trades/{id}")]
        public IActionResult GetTradeById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theTrade = TradesApi.GetTradeById(id.Value);
            if (theTrade == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = theTrade });
        }
        [Route("trades/search")]
        public IActionResult SearchTrade([FromQuery] TradeSearchModel tradeSearch)
        {
            var trades = TradesApi.SearchTrades(tradeSearch);
            if (trades == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = trades });
            //var result=(await TradesApi.GetAllTrades()).Where(trade=>tradeSearch.UserId)
            //return Json(new { success = true, msg = theTrade });
        }

        [Route("exchanges/{userId?}")]
        public async Task<PartialViewResult> Exchanges(string userId)
        {
            var model = new DashboardViewModel();
            if (String.IsNullOrEmpty(userId))
            {
                model.Exchanges = new List<ExchangeListViewModel>();
            }
            else
            {
                model.Exchanges = await ExchangeApi.SearchExchangesByUserId(userId);
            }

            return PartialView("_ExchangeListPartial", model);
        }

        [Route("memberships/{userId?}")]
        public async Task<PartialViewResult> Memberships(string userId)
        {
            var model = new DashboardViewModel();
            if (String.IsNullOrEmpty(userId))
            {
                model.MembershipData = new Membership() { PackagesAvailable = new List<MembershipAvailablePackagesViewModel>(), PackagesPurchased = new List<MembershipPackagesPurchasedViewModel>() };
            }
            else
            {
                model.MembershipData = new Membership
                {
                    PackagesPurchased = await MembershipsApi.GetMembershipPackagesPurchased(),
                    PackagesAvailable = await MembershipsApi.GetAvailableMembershipPackages()
                };
            }

            return PartialView("_Memberships", model);
        }
        [Route("tickets/{userId?}")]
        public async Task<PartialViewResult> Tickets(int? userId)
        {
            var model = new DashboardViewModel();
            if (userId == null)
            {
                model.Tickets = new List<TicketsListViewModel>();
            }
            else
            {
                model.Tickets = await TicketsApi.GetTicketsForClient(userId.Value);
            }

            return PartialView("_ClientTicketsPartial", model);
        }
        [Route("tickets/search/{s?}")]
        public async Task<PartialViewResult> SearchTickets(string s)
        {
            // process the search keywords
            // by date, client name or client id

            var result = new List<TicketsListViewModel>();
            // search by 
            string tv = GetTicketSearchTypeAndValue(s);
            string type = tv.Split('~')[0];
            string value = tv.Split('~')[1];

            if (string.Compare(type, "id", true) == 0)
            {
                if (value.StartsWith("*"))
                {
                    result = await TicketsApi.SearchByClientId(value, WilcardType.Prefix);
                }
                else if (value.EndsWith("*"))
                {
                    result = await TicketsApi.SearchByClientId(value, WilcardType.Suffix);
                }
                else
                {
                    result = await TicketsApi.SearchByClientId(value, WilcardType.None);
                }
            }
            else if (string.Compare(type, "name", true) == 0)
            {
                if (value.StartsWith("*"))
                {
                    result = await TicketsApi.SearchByClientName(value, WilcardType.Prefix);
                }
                else if (value.EndsWith("*"))
                {
                    result = await TicketsApi.SearchByClientName(value, WilcardType.Suffix);
                }
                else
                {
                    result = await TicketsApi.SearchByClientName(value, WilcardType.None);
                }
            }
            // date
            else
            {
                result = await TicketsApi.SearchByDate(value.Split('/')[0], value.Split('/')[1], value.Split('/')[2]);
            }

            var model = new DashboardViewModel();
            model.Tickets = result;


            return PartialView("_ClientTicketsPartial", model);
        }

        private string GetTicketSearchTypeAndValue(string s)
        {
            s = s.ToLower().Trim();
            string type = "", value = "";
            // userid=xxxx
            if (s.StartsWith("userid="))
            {
                type = "id";
                value = s.Substring(7);
                // is wildcard?

            }
            // date=xx/xx/xxxx eg */3/2019 or 26/*/1990
            else if (s.StartsWith("date="))
            {
                type = "date";
                value = s.Substring(5);
            }
            // client name search
            else
            {
                type = "name";
                value = s;
            }
            return $"{type}~{value}";
        }

        [Route("staff/search/{s?}")]
        public async Task<PartialViewResult> SearchStaff(string s)
        {
            s = s.Trim();
            // Process the search string s.
            // if s contains = as in ID=x then search by Id
            // wildcard search involves *
            // mart*  yield all staff with surnames martxxxx
            // * returns all staff

            var model = new DashboardViewModel();
            model.StaffList = new List<StaffListViewModel>();

            if (s == "*") model.StaffList = await StaffApi.GetAllStaff();
            // searching by surname
            else if (s.Contains("*"))
            {
                // a starting or ending *
                if (s.StartsWith("*"))
                {
                    string surnameSuffix = s.Substring(1);
                    if (!String.IsNullOrEmpty(surnameSuffix))
                    {
                        // search by suffix
                        model.StaffList = await StaffApi.SearchByLastNameSuffix(surnameSuffix);
                    }
                }
                else if (s.EndsWith("*"))
                {
                    string surnamePrefix = s.Substring(0, s.Length - 1);
                    if (!String.IsNullOrEmpty(surnamePrefix))
                    {
                        // search by prefix
                        model.StaffList = await StaffApi.SearchByLastNamePrefix(surnamePrefix);
                    }
                }
            }
            // search by ID
            else if (s.ToLower().Contains("id="))
            {
                string idStr = s.Substring(3);
                int id = 0;
                if (int.TryParse(idStr, out id))
                {
                    // search by Id
                    model.StaffList = await StaffApi.SearchStaffById(id);
                }
            }
            // else search by last name
            else
            {
                model.StaffList = await StaffApi.SearchByLastName(s);
            }
            return PartialView("_StaffList", model);
        }

        [Route("staff/{id}")]
        public IActionResult GetStaffById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theStaff = StaffApi.GetStaffById(id.Value);
            if (theStaff == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = theStaff });
        }

        [Route("staff/{id}/notes")]
        public async Task<IActionResult> GetNotesByStaffId(int? id)
        {
            var model = new DashboardViewModel();
            model.NotesForTheSelectedStaff = await StaffApi.GetNotesByStaffId(id.Value);
            return PartialView("_StaffNotesPartial", model);
        }

        [Route("staff/{id}/createNote/{note}")]
        public async Task<IActionResult> CreateStaffNote(int id, string note)
        {
            if (String.IsNullOrEmpty(note)) throw new ArgumentNullException("Note cannot be null!");
            try
            {
                await StaffApi.CreateNewNote(id, note);
                return Json(new { success = true, msg = "Note saved!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, msg = "Error creating note!" });
            }
        }


        [Route("staff/saveorupdate/{id?}")]
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateStaff(int? id, [FromBody] StaffDTO staff)
        {
            try
            {
                string msg = "";
                if (id == null || id.Value == 0)
                {
                    await StaffApi.CreateNewStaff(staff);
                    msg = "Staff record Added";
                }
                else
                {
                    await StaffApi.UpdateStaff(id.Value, staff);
                    msg = "Staff record Updated";
                }
                return Json(new { success = true, msg = msg });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Error updating staff records!" });
            }
        }

        [Route("tickets/{id}")]
        public async Task<IActionResult> GetTicketDataById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");

            var convos = await TicketsApi.GetTicketConversations(id.Value);
            var notes = await TicketsApi.GetAllNotesForTicketClient(id.Value);
            return Json(new { success = true, notes = notes, convos = convos });
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