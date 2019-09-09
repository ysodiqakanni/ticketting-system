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

        public async Task<ISet<Trade>> GetSearchedTradeLines(int? id, DateTime? startDate, 
            DateTime? endDate, 
            ExchangeEnum? exchangeCode, string currencyCode = "")
        {
            var searchResult = await _uow.TradeRepository.FindAllAsync(x => x.UserId == id 
            || (x.DateCreated >= startDate && x.DateCreated <= endDate)
            || x.ExchangeCode == exchangeCode || x.CurrencyCode.ToLower().Contains(currencyCode.ToLower()));
      
            return searchResult.ToHashSet();
        }
    }
}
