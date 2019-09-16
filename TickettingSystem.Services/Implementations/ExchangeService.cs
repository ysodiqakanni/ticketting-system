using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Services.Implementations
{
    public class ExchangeService : IExchangeService
    {
        private readonly IUnitOfWork uow;
        public ExchangeService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public async Task<IList<Exchange>> GetAllKnownExchanges()
        {
            var allKnownExchanges = await uow.ExchangeRepository.GetAllAsync();
            return allKnownExchanges;
        }

        public async Task<IList<Exchange>> SearchExchangesByUserId(string id)
        {
            //var sm = uow.ClientRepository.GetAll().Join(uow.ExchangeRepository.GetAll(), cl => cl.ID, ex => ex.ID,
            //    (cli, exc) => new { Client = cli, Exchange = exc }).ToList().FindAll(x => x.Client.ID.ToString() == id);
            
            var allSearch = await uow.ExchangeRepository.FindAllAsync(x => x.ExchangeUserId.ToString() == id);
            return allSearch.ToList();
        }
    }
}
