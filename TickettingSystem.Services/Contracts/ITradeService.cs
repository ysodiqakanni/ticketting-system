using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using static TickettingSystem.Core.Exchange;

namespace TickettingSystem.Services.Contracts
{
    public interface ITradeService
    {
        Task<Trade> GetById(int id);
        Task<ISet<Trade>> GetSearchedTradeLines(int? id, DateTime? startDate, DateTime? endDate,
            ExchangeEnum? exchangeCode, string currencyCode = "");
    }
}
