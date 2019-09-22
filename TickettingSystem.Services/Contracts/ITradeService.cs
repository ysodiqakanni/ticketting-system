using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.DbModel;
using static TickettingSystem.Core.Exchange;

namespace TickettingSystem.Services.Contracts
{
    public interface ITradeService
    {
        Task<TradeLog> GetById(int id);
        Task<IList<TradeLog>> GetAllTrades();
        Task<IList<TradeLog>> SearchTrades(string searchStr);
        Task<IList<TradeLog>> GetSearchedTradeLines(int? id, DateTime? startDate, DateTime? endDate,
            string exchangeCode="", string currencyCode = "");
        string GetExchangeTypeById(int exchangeTypeId);
        string GetTradeOperationById(int opnId);
    }
}
