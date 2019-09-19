using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.DbModel;

namespace TickettingSystem.Services.Contracts
{
    public interface IExchangeService
    {
        Task<IList<Exchangesusers>> GetAllKnownExchanges();
        Task<IList<Exchangesusers>> SearchExchangesByUserId(string id);
        string GetExchangeTypeById(int exchangeTypeId);
        string GetApiEnteredCount(int exchangeId);
    }
}
