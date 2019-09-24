using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TickettingSystem.ApiHelper;
using TickettingSystem.DTOs;
using TickettingSystem.Models;
using TickettingSystem.Utilities;
using static TickettingSystem.Utilities.AuthorizeUserAttribute;

namespace TickettingSystem.Controllers
{
    [ClaimRequirement("session", "CanReadResource")]
    [Route("home")]
    public class HomeController : Controller
    {
        private StaffApi staffApi;
        private ClientsApi clientApi;
        private ExchangeApi exchangeApi;
        private TicketsApi ticketsApi;
        private TradesApi tradesApi;
        private readonly AppSettings _appSettings;
        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            staffApi = new StaffApi(_appSettings);
            clientApi = new ClientsApi(_appSettings);
            exchangeApi = new ExchangeApi(_appSettings);
            ticketsApi = new TicketsApi(_appSettings);
            tradesApi = new TradesApi(_appSettings);
        }
        [Route("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var allClients = await clientApi.GetAllClients();
            ViewBag.AllClients = allClients;

            var model = new DashboardViewModel();
            model.Clients = allClients; // await ClientsApi.GetAllClients();///.SearchClients("");
            model.Trades = new List<TradeDTO>(); // should be empty until a client is selected await TradesApi.GetAllTrades();

            model.NotesForSelectedClient = new List<NoteListViewModel>(); // No notes displayed when no client is selected! await clientApi.GetAllNotes();
            model.MembershipData = new Membership
            {
                PackagesAvailable = new List<MembershipAvailablePackagesViewModel>(),
                PackagesPurchased = new List<MembershipPackagesPurchasedViewModel>()
            };
            model.StaffList = await staffApi.GetAllStaff();
            ViewBag.AllStaff = model.StaffList;
            model.NotesForTheSelectedStaff = new List<StaffNoteViewModel>() { }; // since no staff has been selected yet
            // initially, no client is selected!
            // so search results (D) should contain each of the known exchanges 
            model.Exchanges = await exchangeApi.GetAllKnownExchanges();
            ViewBag.ExchangeTypes = await exchangeApi.GetAllExchangeTypes();
            model.Tickets = await ticketsApi.GetLastTenTickets();
            model.TicketConversations = new List<TicketConversationViewModel>();
            model.NotesForSelectedTicketClient = new List<NoteListViewModel>();
            model.Languages = await clientApi.GetAllLanguages();

            return View(model);
        }

        [Route("clients/{id}")]
        public async Task<IActionResult> GetClientById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theClient = await clientApi.GetClientById(id.Value);
            if (theClient == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = theClient });
        }

        [Route("clients/{id}/notes")]
        public async Task<IActionResult> GetNotesByClientId(int? id)
        {
            var model = new DashboardViewModel();
            model.NotesForSelectedClient = await clientApi.GetNotesByClientId(id.Value);
            return PartialView("_NotesPartial", model);
        }

        [Route("resetpassword/{id}")]
        public async Task<IActionResult> ResetPassword(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            try
            {
                await clientApi.ResetPassword(id.Value);
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
                    await clientApi.Update(updateModel);
                    return Json(new { success = true, msg = "Data updated sucessfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = "an error occured while updating records!" });
                }
            }
            string errorMsg = "";
            foreach (var modelState in ModelState.Values)
            {
                errorMsg += "\n" + String.Join('\n', modelState.Errors.Select(e => e.ErrorMessage));
            }
            return Json(new { success = false, msg = errorMsg });
        }

        [Route("createNote/{note}/{userId}")]
        public async Task<IActionResult> createNote(string note, string userId)
        {
            if (String.IsNullOrEmpty(note)) throw new ArgumentNullException("Note cannot be null!");
            try
            {
                //string userId = "74"; // pass with js from the page
                string createdBy = "system";
                string modifiedBy = "system";
                Dictionary<string, string> noteModel = new Dictionary<string, string>() { { "Note", note }, { "Modifiedby", modifiedBy }, { "Createdby", createdBy }, { "Userid", userId } };

                await clientApi.CreateNewNote(JsonConvert.SerializeObject(noteModel));
                return Json(new { success = true, msg = "Note saved!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Error creating note!" });
            }
        }

        [Route("SearchClients/{searchStr?}")]
        public async Task<PartialViewResult> SearchClients(string searchStr = "")
        {
            var model = new DashboardViewModel();
            model.Clients = await clientApi.SearchClients(searchStr);
            return PartialView("_ClientListPartial", model);
        }

        //[Route("notes")]
        //public async Task<PartialViewResult> Notes()
        //{
        //    var model = new DashboardViewModel();
        //    model.NotesForSelectedClient = await clientApi.GetAllNotes();
        //    return PartialView("_NotesPartial", model);
        //} 
        [Route("trades/{id}")]
        public async Task<IActionResult> GetTradeById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theTrade = await tradesApi.GetTradeById(id.Value);
            if (theTrade == null) return Json(new { success = false, msg = "record not found!" });
            return Json(new { success = true, msg = theTrade });
        }
        [Route("trades/search")]
        public async Task<IActionResult> SearchTrade([FromQuery] TradeSearchModel tradeSearch)
        {
            var trades = await tradesApi.SearchTrades(tradeSearch);
            var model = new DashboardViewModel();
            model.Trades = trades;
            return PartialView("_TradeListPartial", model); 
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
                model.Exchanges = await exchangeApi.SearchExchangesByUserId(userId);
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
                model.Tickets = await ticketsApi.GetTicketsForClient(userId.Value);
            }

            return PartialView("_ClientTicketsPartial", model);
        }
        [Route("tickets/search/{s?}")]
        public async Task<PartialViewResult> SearchTickets(string s)
        {
            if (string.IsNullOrEmpty(s))
                s = "*";  // return all tickets

            var result = new List<TicketsListViewModel>();
            // search by 
            string tv = GetTicketSearchTypeAndValue(s);
            string type = tv.Split('~')[0];
            string value = tv.Split('~')[1];

            if (string.Compare(type, "id", true) == 0)
            {
                if (value == "*") // situation where user enters * to search for all tickets
                {
                    result = await ticketsApi.GetLastTenTickets();
                }
                else if (value.StartsWith("*"))
                {
                    result = await ticketsApi.SearchByClientId(value, WilcardType.Prefix);
                }
                else if (value.EndsWith("*"))
                {
                    result = await ticketsApi.SearchByClientId(value, WilcardType.Suffix);
                }
                else
                {
                    result = await ticketsApi.SearchByClientId(value, WilcardType.None);
                }
            }
            else if (string.Compare(type, "name", true) == 0)
            {
                if(value == "*") // situation where user enters * to search for all tickets
                {
                    result = await ticketsApi.GetLastTenTickets();
                }
                else if (value.StartsWith("*"))
                {
                    result = await ticketsApi.SearchByClientName(value, WilcardType.Prefix);
                }
                else if (value.EndsWith("*"))
                {
                    result = await ticketsApi.SearchByClientName(value, WilcardType.Suffix);
                }
                else
                {
                    // not a wildcard search, so search by name 
                    result = await ticketsApi.SearchByClientName(value, WilcardType.None);
                }
            }
            // date
            else
            {
                result = await ticketsApi.SearchByDate(value.Split('/')[0], value.Split('/')[1], value.Split('/')[2]);
            }

            var model = new DashboardViewModel();
            model.Tickets = result;


            return PartialView("_ClientTicketsPartial", model);
        }

        [Route("tickets/close/{id}")] 
        public async Task<IActionResult> CloseTicket(int id)
        {
            try
            {
                await ticketsApi.CloseTicket(id);
                return Json(new { success = true, msg = "Ticket closed sucessfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Error closing ticket!" });
            }
        }

        [Route("tickets/update")]
        [HttpPost]
        public async Task<IActionResult> UpdateTicket([FromBody] TicketUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ticketsApi.UpdateTicket(model);
                    return Json(new { success = true, msg = "Data updated sucessfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = "an error occured while updating records!" });
                }
            }
            return Json(new { success = false, msg = "Fill in all required fields" });
        }

        [Route("ticket/{id}/createNote/{note}")]
        public async Task<IActionResult> CreateTicketNote(int id, string note)
        {
            if (String.IsNullOrEmpty(note)) throw new ArgumentNullException("Note cannot be null!");
            try
            {
                throw new Exception("Ed");
                // await ticketsApi.CreateNewNote(id, note);
                return Json(new { success = true, msg = "Note saved!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, msg = "Error creating note!" });
            }
        }

        [Route("tickets/{id}")]
        public async Task<IActionResult> GetTicketDataById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");

            var convos = await ticketsApi.GetTicketConversations(id.Value);
            var notes = await ticketsApi.GetAllNotesForTicketClient(id.Value);
            return Json(new { success = true, notes = notes, convos = convos });
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

            if (s == "*") model.StaffList = await staffApi.GetAllStaff();
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
        public async Task<IActionResult> GetStaffById(int? id)
        {
            if (id == null) throw new ArgumentNullException("Invalid request sent!");
            var theStaff = await StaffApi.GetStaffById(id.Value);
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