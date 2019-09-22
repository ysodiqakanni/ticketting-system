using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickettingSystem.Core;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Data.Implementations;
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

        public async Task<IList<Exchangesusers>> GetAllKnownExchanges()
        {
            var allKnownExchanges = await uow.ExchangeRepository.GetAllAsync();
            return allKnownExchanges;
        }

        public async Task<IList<Exchangesusers>> SearchExchangesByUserId(string id)
        {
            var allSearch = await uow.ExchangeRepository.FindAllAsync(x => x.Exchangeuuserid.ToString() == id);
            return allSearch.ToList();
        }

        public string GetExchangeTypeById(int exchangeTypeId)
        {
            return uow.ExchangeTypeRepository.Get(exchangeTypeId)?.Name;
        }

        public string GetApiEnteredCount(int exchangeId)
        {
            int count = 0;
            string countVal = string.Empty;
            var ap = uow.ExchangeRepository.Find(x => x.EuId == exchangeId).FirstOrDefault();
            if (ap != null)
            {
                if (!string.IsNullOrEmpty(ap.Exchangeuapi1) && !string.IsNullOrEmpty(ap.Exchangeuapi2))
                {
                    count = 2;
                }
                if ((!string.IsNullOrEmpty(ap.Exchangeuapi1) && string.IsNullOrEmpty(ap.Exchangeuapi2))
                    || (string.IsNullOrEmpty(ap.Exchangeuapi1) && !string.IsNullOrEmpty(ap.Exchangeuapi2)))
                {
                    count = 1;
                }
            }

            switch (count)
            {
                case 2:
                    countVal = "Two";
                    break;
                case 1:
                    countVal = "One";
                    break;
                case 0:
                    countVal = "NONE ENTERED";
                    break;
                default:
                    break;
            }


            return countVal;
        }
    }
}