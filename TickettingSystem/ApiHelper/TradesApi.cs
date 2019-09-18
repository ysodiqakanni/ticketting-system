﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public class TradesApi
    {
        static string apiBaseUrl = "https://localhost:44355/api/v1/";
        public TradesApi()
        {
        }
        public async static Task<List<TradeDTO>> SearchTrades(TradeSearchModel tradeSearch)
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
        public async static Task<TradeDTO> GetTradeById(int id)
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