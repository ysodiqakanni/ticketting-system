using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public class TicketsApi
    {
        public static Task<List<TicketsListViewModel>> GetLastTenTickets()
        {
            // return last 10 tickets from the db
            var tickets = new List<TicketsListViewModel>
            {
                new TicketsListViewModel{Id = 1, ClientName ="John Wlaters", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 100 },
                new TicketsListViewModel{Id = 2, ClientName ="Bakers Wlaters", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 89 },
                new TicketsListViewModel{Id = 3, ClientName ="Roy Mesh", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 100 },
                new TicketsListViewModel{Id = 4, ClientName ="White Wlaters", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 200 },
                new TicketsListViewModel{Id = 5, ClientName ="Ola Johnson", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 435 },
            };
            return Task.Run(() => { return tickets; });
        }
        public static Task<List<TicketsListViewModel>> GetTicketsForClient(int clientId)
        {
            // return all ticketc for a client
            var tickets = new List<TicketsListViewModel>
            {
                new TicketsListViewModel{Id = 1, ClientName ="John Wlaters", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 100 },
                new TicketsListViewModel{Id = 2, ClientName ="Bakers Wlaters", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 89 },
                new TicketsListViewModel{Id = 3, ClientName ="Roy Mesh", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 100 },
                new TicketsListViewModel{Id = 4, ClientName ="White Wlaters", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 200 },
                new TicketsListViewModel{Id = 5, ClientName ="Ola Johnson", DateEnabled = DateTime.Now, Description = "Lorem ipsum dolor sit amet.", Price = 435 },
            };
            return Task.Run(() => { return tickets; });
        }

        public static Task<List<TicketsListViewModel>> SearchByClientId(string value, WilcardType prefix)
        {
            var tickets = new List<TicketsListViewModel>
            {
                new TicketsListViewModel{Id = 1, ClientName ="John Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from Id search", Price = 100 },
                new TicketsListViewModel{Id = 2, ClientName ="Bakers Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from Id search", Price = 89 },
                new TicketsListViewModel{Id = 3, ClientName ="Roy Mesh", DateEnabled = DateTime.Now, Description = "Data retrieved from Id search.", Price = 100 },
                new TicketsListViewModel{Id = 4, ClientName ="White Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from Id search", Price = 200 },
                new TicketsListViewModel{Id = 5, ClientName ="Ola Johnson", DateEnabled = DateTime.Now, Description = "Data retrieved from Id search.", Price = 435 },
            };
            return Task.Run(() => { return tickets; });
        }

        public static Task<List<TicketsListViewModel>> SearchByClientName(string value, WilcardType prefix)
        {
            // search through first and surname
            var tickets = new List<TicketsListViewModel>
            {
                new TicketsListViewModel{Id = 1, ClientName ="John Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from name search", Price = 100 },
                new TicketsListViewModel{Id = 2, ClientName ="Bakers Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from name search", Price = 89 },
                new TicketsListViewModel{Id = 3, ClientName ="Roy Mesh", DateEnabled = DateTime.Now, Description = "Data retrieved from name search.", Price = 100 },
                new TicketsListViewModel{Id = 4, ClientName ="White Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from name search", Price = 200 },
                new TicketsListViewModel{Id = 5, ClientName ="Ola Johnson", DateEnabled = DateTime.Now, Description = "Data retrieved from name search.", Price = 435 },
            };
            return Task.Run(() => { return tickets; });
        }
        public static Task<List<TicketsListViewModel>> SearchByDate(string day, string month, string year)
        {
            // not that either or all of day, month and year could be *
            // so date=*/8/2019 would be all data for august 2019 - */*/2019 would return all data from 2019.

            var tickets = new List<TicketsListViewModel>
            {
                new TicketsListViewModel{Id = 1, ClientName ="John Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from date search", Price = 100 },
                new TicketsListViewModel{Id = 2, ClientName ="Bakers Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from date search", Price = 89 },
                new TicketsListViewModel{Id = 3, ClientName ="Roy Mesh", DateEnabled = DateTime.Now, Description = "Data retrieved from name date.", Price = 100 },
                new TicketsListViewModel{Id = 4, ClientName ="White Wlaters", DateEnabled = DateTime.Now, Description = "Data retrieved from date search", Price = 200 },
                new TicketsListViewModel{Id = 5, ClientName ="Ola Johnson", DateEnabled = DateTime.Now, Description = "Data retrieved from date search.", Price = 435 },
            };
            return Task.Run(() => { return tickets; });
        }
        public static Task<List<TicketConversationViewModel>> GetTicketConversations(int ticketId)
        {
            // The convos are by them tickets
            // so retrieve all tickets with parentid = ticketId
            var convos = new List<TicketConversationViewModel>
            {
                new TicketConversationViewModel{Content="hello support", DateCreated= DateTime.Now, CreatedByClient = true},
                new TicketConversationViewModel{Content="I need an assistance", DateCreated= DateTime.Now, CreatedByClient = true},
                new TicketConversationViewModel{Content="hi client", DateCreated= DateTime.Now, CreatedByClient = false},
                new TicketConversationViewModel{Content="how do you do", DateCreated= DateTime.Now, CreatedByClient = false},
                new TicketConversationViewModel{Content="My name is Maurie, how may I help you today?", DateCreated= DateTime.Now, CreatedByClient = false},
                new TicketConversationViewModel{Content="I can't log in, please help!", DateCreated= DateTime.Now, CreatedByClient = true},
            };
            return Task.Run(() => { return convos; });
        }
        public static async Task<List<NoteListViewModel>> GetAllNotesForTicketClient(int ticketId)
        { 
            return new List<NoteListViewModel>()
            {
                new NoteListViewModel{Content = "So Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"},
                new NoteListViewModel{Content = "Full Notes go here. Ask me why it should go in here and I will ask you why it shouldn't. Not all issues deseve questions and not all questions deserve answers. Tainkyu"}
            };
        }
    }
}
