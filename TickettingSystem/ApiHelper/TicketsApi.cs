﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Models;
using TickettingSystem.Utilities;

namespace TickettingSystem.ApiHelper
{
    public class TicketsApi
    {
        private readonly AppSettings _appSettings;

        private string baseUrl;
        public TicketsApi(AppSettings appSettings)
        {
            _appSettings = appSettings;
            baseUrl = _appSettings.BaseUrl;
        }
         
        public async Task<List<TicketsListViewModel>> GetLastTenTickets()
        {
            // return last 10 tickets from the db
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("tickets");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TicketsListViewModel>>();
                return responseBody;
            } 
        }
        public async Task<List<TicketsListViewModel>> GetTicketsForClient(int clientId)
        {
            // return all tickets for a client
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"tickets/search/{clientId}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TicketsListViewModel>>();
                return responseBody;
            } 
        }

        public async Task<List<TicketsListViewModel>> SearchByClientId(string value, WilcardType wilcard)
        {
            string url = string.Empty;
            switch (wilcard)
            {
                case WilcardType.None:
                    url = $"tickets/search/{value}";
                    break;
                case WilcardType.Prefix:
                    url = $"tickets/search/{value.TrimStart('*')}/wildcardprefix";
                    break;
                case WilcardType.Suffix:
                    url = $"tickets/search/{value.TrimEnd('*')}/wildcardsuffix";
                    break;
                default:
                    return new List<TicketsListViewModel>();
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync(url);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TicketsListViewModel>>();
                return responseBody;
            } 
        }

        public async Task<List<TicketsListViewModel>> SearchByClientName(string value, WilcardType wilcard)
        {
            string url = string.Empty;
            switch (wilcard)
            {
                case WilcardType.None:
                    url = $"tickets/search/{0}/{value}";
                    break;
                case WilcardType.Prefix:
                    url = $"tickets/wildcardsearch/prefix/{value.TrimStart('*')}";
                    break;
                case WilcardType.Suffix:
                    url = $"tickets/wildcardsearch/suffix/{value.TrimEnd('*')}";
                    break;
                default:
                    return new List<TicketsListViewModel>();
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync(url);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TicketsListViewModel>>();
                return responseBody;
            } 
        }
        public Task<List<TicketsListViewModel>> SearchByDate(string day, string month, string year)
        {
            // not that either or all of day, month and year could be *
            // so date=*/8/2019 would be all data for august 2019 - */*/2019 would return all data from 2019.

            var tickets = new List<TicketsListViewModel>
            {
                new TicketsListViewModel{Id = 1, ClientName ="John Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from date search", Price = 100, AssignedToStaffId = 2 },
                new TicketsListViewModel{Id = 2, ClientName ="Bakers Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from date search", Price = 89 , AssignedToStaffId = 2},
                new TicketsListViewModel{Id = 3, ClientName ="Roy Mesh", DateEnabled = DateTime.Now, Description = "Data retrieved from name date.", Price = 100, AssignedToStaffId = 2 },
                new TicketsListViewModel{Id = 4, ClientName ="White Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from date search", Price = 200 , AssignedToStaffId = 2},
                new TicketsListViewModel{Id = 5, ClientName ="Ola Johnson", DateEnabled = DateTime.Now, Description = "Data retrieved from date search.", Price = 435, AssignedToStaffId = 2 },
            };
            return Task.Run(() => { return tickets; });
        }
        public async Task<List<TicketConversationViewModel>> GetTicketConversations(int ticketId)
        {
            // The convos are by them tickets
            // so retrieve all tickets with parentid = ticketId
            var url = $"tickets/conversations/{ticketId}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync(url);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TicketConversationViewModel>>();
                return responseBody;
            } 
        }

        public async Task<string> CreateNewNote(int? staffId, string note, int ticketId)
        {
            using (HttpClient client = new HttpClient())
            {
                if (staffId == null) staffId = 0; 
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"tickets/{ticketId}/createnote/{staffId}/{note}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsStringAsync();

                return responseBody;
            }
        }
        public async Task UpdateNote(string noteData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(noteData, Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("tickets/notes", httpContent);
                msg.EnsureSuccessStatusCode();
            }
            return;
        }

        public async Task<List<NoteListViewModel>> GetAllNotesForTicketClient(int ticketId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"tickets/notes/{ticketId}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<NoteListViewModel>>();
                return responseBody;
            }
 
        }
        public async Task CloseTicket(int ticketId)
        {
            // close the ticket
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"tickets/close/{ticketId}");
                msg.EnsureSuccessStatusCode(); 
                return;
            }
        }
        public async Task UpdateTicket(TicketUpdateViewModel model)
        {
            // add a note and or reassign staff
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync($"tickets/update/{model.Id}/{model.AssignedStaffId}/{model.Note}");
                msg.EnsureSuccessStatusCode();
                return;
            }
        }
        public async Task SendResponse(int ticketId, string note)
        {
            // add a note and or reassign staff
        }
    }
}