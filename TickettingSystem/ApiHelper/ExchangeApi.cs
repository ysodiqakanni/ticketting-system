 

using System;
using System.Collections.Generic;

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TickettingSystem.DTOs;
using TickettingSystem.Models;
using TickettingSystem.Utilities;

namespace TickettingSystem.ApiHelper
{

    public class ExchangeApi
    {
        private readonly AppSettings _appSettings;

        private string baseUrl;
        public ExchangeApi(AppSettings appSettings)
        {
            _appSettings = appSettings;
            baseUrl = _appSettings.BaseUrl;
        } 
        public async Task<List<ExchangeListViewModel>> GetAllKnownExchanges()
        {
            var knownExchanges = (await GetAllExchangeTypes()).Select(x => new ExchangeListViewModel { ExchangeName = x, APIsEntered = null }).ToList();
            return knownExchanges; 
             
        }
        public async Task<List<ExchangeListViewModel>> SearchExchangesByUserId(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                HttpResponseMessage msg = await client.GetAsync("exchange/search?userId="+id);
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<ExchangeListViewModel>>();
                return responseBody;
            } 
        }
        public Task<List<string>> GetAllExchangeTypes()  
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