using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Services.Contracts;


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
    }
}
