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
    public class TradesApi
    {
        public TradesApi()
        {
        }
        public static async Task<List<TradeDTO>> GetAllTrades()
        {
            //var trades = new List<TradeDTO>
            //{
            //    new TradeDTO{ID=1,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //    new TradeDTO{ID=2,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //     new TradeDTO{ID=3,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //      new TradeDTO{ID=4,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //       new TradeDTO{ID=5,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //        new TradeDTO{ID=6,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //};
            //return Task.Run(() => { return trades; });

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1662/api/v1/");

                HttpResponseMessage msg = await client.GetAsync("trade");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<List<TradeDTO>>();
                return responseBody;
            }
        }


        public static async Task<TradeDTO> GetTradeById(int id)
        {
            //return Task.Run(() =>
            //{
            //    return new TradeDTO
            //    {
            //        ID = id,
            //        Exchange = "",
            //        Operation = "",
            //        UserId = 2,
            //        CreatedOn = DateTime.Now,
            //        Price = 1000

            //    };
            //});

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1662/api/v1/");

                HttpResponseMessage msg = await client.GetAsync($"trade/{id}");
                msg.EnsureSuccessStatusCode();
                var responseBody = await msg.Content.ReadAsAsync<TradeDTO>();
                return responseBody;
            }
        }


        public static async Task<List<TradeViewModel>> SearchTrades(string searchStr)
        {
            //var clients = new List<TradeViewModel>
            //{
            //    new TradeViewModel{ID=1,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //  new TradeViewModel{ID=2,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //     new TradeViewModel{ID=3,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //      new TradeViewModel{ID=4,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //       new TradeViewModel{ID=5,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //        new TradeViewModel{ID=6,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
            //};
            //return Task.Run(() => { return clients; });
            using (HttpClient client = new HttpClient())
            {

                var builder = new UriBuilder("http://localhost:5000/api/v1/trade/search");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["searchStr"] = searchStr;
                builder.Query = query.ToString();
                string url = builder.ToString();
                HttpResponseMessage msg = await client.GetAsync(url);
                msg.EnsureSuccessStatusCode();
                var responsebody = await msg.Content.ReadAsAsync<List<TradeViewModel>>();
                return responsebody;
            }
        }
    }
}

