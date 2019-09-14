using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public static class ExchangeApi
    {
        public static async Task<List<ExchangeGetViewDTO>> GetAllKnownExchanges()
        {
            // This should return only the name column
            //var exchanges = new List<ExchangeListViewModel>
            //{
            //    new ExchangeListViewModel{ExchangeName = "Lorem"},
            //    new ExchangeListViewModel{ExchangeName = "Lorem"},
            //    new ExchangeListViewModel{ExchangeName = "Lorem"}
            //};
            //return Task.Run(() => { return exchanges; });

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1662/api/v1/");

                HttpResponseMessage msg = await client.GetAsync("exchange");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ExchangeGetViewDTO>>();
                return responseBody;
            }

        }
        public static Task<List<ExchangeListViewModel>> SearchExchangesByUserId(string id)
        {
            var exchanges = new List<ExchangeListViewModel>
            {
                new ExchangeListViewModel{ExchangeName = "Lorem", DateEnabled = DateTime.Now, APIsEntered = "APi-SomeApis"},
                new ExchangeListViewModel{ExchangeName = "Lorem", DateEnabled = DateTime.Now, APIsEntered = null},
                new ExchangeListViewModel{ExchangeName = "Lorem", DateEnabled = DateTime.Now, APIsEntered = "APi-SomeApis"}
            };
            return Task.Run(() => { return exchanges; });

        }
    }
}
