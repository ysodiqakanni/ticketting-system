using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{

    public static class ExchangeApi
    {
        static string apiBaseUrl = "https://localhost:5001/api/v1/";
        public static async Task<List<ExchangeListViewModel>> GetAllKnownExchanges()
        {
            // This should return only the name column
            //var exchanges = new List<ExchangeListViewModel>
            //{
            //    new ExchangeListViewModel{ExchangeName = "Lorem"},
            //    new ExchangeListViewModel{ExchangeName = "Lorem"},
            //    new ExchangeListViewModel{ExchangeName = "Lorem"}
            //};
            

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("exchange");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ExchangeListViewModel>>();
                return responseBody;
            }
            //return Task.Run(() => { return exchanges; });
        }
        public static async Task<List<ExchangeListViewModel>> SearchExchangesByUserId(string id)
        {
            //var exchanges = new List<ExchangeListViewModel>
            //{
            //    new ExchangeListViewModel{ExchangeName = "Lorem", DateEnabled = DateTime.Now, APIsEntered = "APi-SomeApis"},
            //    new ExchangeListViewModel{ExchangeName = "Lorem", DateEnabled = DateTime.Now, APIsEntered = null},
            //    new ExchangeListViewModel{ExchangeName = "Lorem", DateEnabled = DateTime.Now, APIsEntered = "APi-SomeApis"}
            //};
            //return Task.Run(() => { return exchanges; });

            using (HttpClient client = new HttpClient())
            {
                var builder = new UriBuilder("http://localhost:5000/api/v1/exchange/search");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["userId"] = id;
                builder.Query = query.ToString();
                string url = builder.ToString();
                HttpResponseMessage msg = await client.GetAsync(url);
                msg.EnsureSuccessStatusCode();
                var responsebody = await msg.Content.ReadAsAsync<List<ExchangeListViewModel>>();
                return responsebody;
            }
        }
    }
}