using System;
using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{

    public static class ExchangeApi
    {
        public static Task<List<ExchangeListViewModel>> GetAllKnownExchanges()
        {
            // This should return only the name column
            var exchanges = new List<ExchangeListViewModel>
            {
                new ExchangeListViewModel{ExchangeName = "Lorem"},
                new ExchangeListViewModel{ExchangeName = "Lorem"},
                new ExchangeListViewModel{ExchangeName = "Lorem"}
            };
            return Task.Run(() => { return exchanges; });

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
        public static Task<List<string>> GetAllExchanges()  // Exchange types actually
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