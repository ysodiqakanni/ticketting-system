using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;
using static TickettingSystem.Core.Exchange;

namespace TickettingSystem.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly IUnitOfWork _uow;

        public TradeService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<TradeLog> GetById(int id)
        {
            return await _uow.TradeRepository.GetAsync(id);   
        }

        public async Task<IList<TradeLog>> GetAllTrades()
        {
            var allTrades = _uow.TradeRepository.QueryAll().OrderByDescending(x => x.TradePlaceDate).ToList().GetRange(0, 9);
            return allTrades;
        }

        public async Task<IList<TradeLog>> SearchTrades(string searchStr)
        {
            var trades =  _uow.TradeRepository.Find(x => x.TradeId.ToString() == searchStr).OrderByDescending(x => x.TradePlaceDate).ToList().Take(10);
            // Todo: fix below
            //|| x.Exchange.ToLower().Contains(searchStr.ToLower()) || x.CurrencyCode.ToLower().Contains(searchStr.ToLower()));
            return trades.ToList();
        }

        public async Task<IList<TradeLog>> GetSearchedTradeLines(int? id, DateTime? startDate, DateTime? endDate, 
            string exchangeCode = "", string currencyCode = "")
        {
            var trades = _uow.TradeRepository.QueryAll();
            if (id != null)
                trades = trades.Where(x => x.TraderUid.ToString() == id.ToString());

            if (startDate != null && startDate != default(DateTime))
                trades = trades.Where(x => x.TradePlaceDate.Date >= startDate.Value.Date);
            if (endDate != null && endDate != default(DateTime))
                trades = trades.Where(x => x.TradePlaceDate.Date <= endDate.Value.Date);

            if (!string.IsNullOrEmpty(currencyCode))
                trades = trades.Where(t => t.CurrencyPair.ToLower().Contains(currencyCode.ToLower()));

            if (!String.IsNullOrEmpty(exchangeCode))
            {
                // get ids of exchanges with names containing exchangeCode
                // check if ids contains t.id
                exchangeCode = exchangeCode.ToLower();
                var ids = _uow.ExchangeTypeRepository.QueryAll().Where(e => e.Name.Contains(exchangeCode)).Select(e => e.Id).ToList();
                trades = trades.Where(t => ids.Contains(t.ExchangeFromTypeId.Value) || ids.Contains(t.ExchangeToTypeId.Value));
            }

            trades = trades.OrderByDescending(x => x.TradePlaceDate).Take(10);

            return trades.ToList();
        }

        public string GetExchangeTypeById(int exchangeTypeId)
        {
            return _uow.ExchangeTypeRepository.Get(exchangeTypeId)?.Name;
        }
        public string GetTradeOperationById(int opnId)
        {
            // buy sell
            switch (opnId)
            {
                case 1:
                    return "Buy";
                case 2:
                    return "Sell";
                case 3:
                    return "Transfer";

                default:
                    return "Null";
            } 
        }
    }
}
