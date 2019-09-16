﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickettingSystem.DTOs;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public class TradesApi
    {
        public TradesApi()
        {
        }
        public static Task<List<TradeDTO>> SearchTrades(TradeSearchModel tradeSearch)
        {
            // search for trade using the tradesearch properties
            var trades = new List<TradeDTO>
            {
                new TradeDTO{ID=1,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
                new TradeDTO{ID=2,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
                 new TradeDTO{ID=3,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
                  new TradeDTO{ID=4,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
                   new TradeDTO{ID=5,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
                    new TradeDTO{ID=6,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
            };
            return Task.Run(() => { return trades; });
        }
        public static Task<TradeDTO> GetTradeById(int id)
        {
            return Task.Run(() =>
            {
                return new TradeDTO
                {
                    ID = id,
                    Exchange = "",
                    Operation = "",
                    UserId = 2,
                    CreatedOn=DateTime.Now,
                    Price=1000
              
                };
            });
        }
        public static Task<List<TradeViewModel>> GetAllTrades(string searchStr)
        {
            var clients = new List<TradeViewModel>
            {
                new TradeViewModel{ID=1,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
              new TradeViewModel{ID=2,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
                 new TradeViewModel{ID=3,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
                  new TradeViewModel{ID=4,Exchange="Lorem ipsum dolor sit amet",Operation="SELL",UserId=1,CreatedOn=DateTime.Now,Price=600},
                   new TradeViewModel{ID=5,Exchange="Lorem ipsum dolor sit amet",Operation="TRANSFER",UserId=1,CreatedOn=DateTime.Now,Price=600},
                    new TradeViewModel{ID=6,Exchange="Lorem ipsum dolor sit amet",Operation="BUY",UserId=1,CreatedOn=DateTime.Now,Price=600},
            };
            return Task.Run(() => { return clients; }); 
        }
    }
}
