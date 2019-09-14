using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;

namespace TickettingSystem.Services.Contracts
{
    public interface IExchangeService
    {
        Task<IList<Exchange>> GetAllKnownExchanges();
        Task<IList<Exchange>> SearchExchangesByUserId(string id);
    }
}
