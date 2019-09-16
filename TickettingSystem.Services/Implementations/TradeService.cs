using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
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


        public async Task<Trade> GetById(int id)
        {
            return await _uow.TradeRepository.GetAsync(id);   
        }

        public async Task<IList<Trade>> GetAllTrades()
        {
            var allTrades = _uow.TradeRepository.QueryAll().OrderByDescending(x => x.DateModified).ToList().GetRange(0, 9);
            return allTrades;
        }

        public async Task<IList<Trade>> SearchTrades(string searchStr)
        {
            var trades = await _uow.TradeRepository.FindAllAsync(x => x.UserId.ToString() == searchStr
            || x.Exchange.ToLower().Contains(searchStr.ToLower()) || x.CurrencyCode.ToLower().Contains(searchStr.ToLower()));
            return trades.ToList();
        }

        public async Task<IList<Trade>> GetSearchedTradeLines(int? id, DateTime? startDate, DateTime? endDate, 
            string exchangeCode = "", string currencyCode = "")
        {
            var trades = await _uow.TradeRepository.FindAllAsync(x => x.UserId.ToString() == id.ToString()
            || (x.DateCreated >= startDate && x.DateCreated <= endDate)
            || x.Exchange.ToLower().Contains(exchangeCode.ToLower()) || x.CurrencyCode.ToLower().Contains(currencyCode.ToLower()));
            return trades.ToList();
        }
        
    }
}
