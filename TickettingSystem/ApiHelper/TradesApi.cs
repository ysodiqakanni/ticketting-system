using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;
using TickettingSystem.Utilities;

namespace TickettingSystem.ApiHelper
{
    public class TradesApi
    {
        private readonly AppSettings _appSettings;

        private string apiBaseUrl;
        public TradesApi(AppSettings appSettings)
        {
            _appSettings = appSettings;
            apiBaseUrl = _appSettings.BaseUrl;
        }
        public async Task<List<TradeDTO>> SearchTrades(TradeSearchModel tradeSearch)
        {
            // return the last 10 trades for the client/user
            // search for trade using the tradesearch properties

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync($"trade/query?id={tradeSearch.UserId}&startDate={tradeSearch.FromDateTime}&endDate={tradeSearch.ToDateTime}&exchange={tradeSearch.Exchange}&currencyCode={tradeSearch.currencyCode}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TradeDTO>>();
                return responseBody;
            } 
        }
        public async Task<TradeDTO> GetTradeById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage msg = await client.GetAsync($"trade/{id}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<TradeDTO>();
                return responseBody;
            } 
        } 
    }
}