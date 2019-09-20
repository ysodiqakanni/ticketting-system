 

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
        static string apiBaseUrl = "https://localhost:44355/api/v1/";
        public static async Task<List<ExchangeListViewModel>> GetAllKnownExchanges()
        {
            var knownExchanges = (await GetAllExchangeTypes()).Select(x => new ExchangeListViewModel { ExchangeName = x, APIsEntered = null }).ToList();
            return knownExchanges; 
             
        }
        public static async Task<List<ExchangeListViewModel>> SearchExchangesByUserId(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync("exchange/search?userId="+id);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ExchangeListViewModel>>();
                return responseBody;
            } 
        }
        public static Task<List<string>> GetAllExchangeTypes()  
        {
            List<string> exchanges = new List<string>();
            exchanges.Add("CoinBase");
            exchanges.Add("Poloniex");
            exchanges.Add("Livecoin");
            exchanges.Add("Bittrex");
            exchanges.Add("Coinbene");
            exchanges.Add("Cex");

            return Task.Run(() => { return exchanges; });
        }
    }
}