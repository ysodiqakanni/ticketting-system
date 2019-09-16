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
    }
}
