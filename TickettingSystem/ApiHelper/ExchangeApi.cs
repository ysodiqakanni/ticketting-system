using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TickettingSystem.Models;

namespace TickettingSystem.ApiHelper
{
    public class ExchangeApi
    {
        public ExchangeApi()
        {
        }
       public static Task<List<ExchangeViewModel>> getAllExchange()
        {
            var exchanges = new List<ExchangeViewModel>
            {
                new ExchangeViewModel{Name="Exchange 1",Enabled=DateTime.Now,EnteredApi="EnteredApi 1",VerifiedConnection="Verified connection 1"},
                new ExchangeViewModel{Name="Exchange 2",Enabled=DateTime.Now,EnteredApi="EnteredApi 2",VerifiedConnection="Verified connection 2"},
                new ExchangeViewModel{Name="Exchange 3",Enabled=DateTime.Now,EnteredApi="EnteredApi 3",VerifiedConnection="Verified connection 3"},
                new ExchangeViewModel{Name="Exchange 4",Enabled=DateTime.Now,EnteredApi="EnteredApi 4",VerifiedConnection="Verified connection 4"},
            };
            return Task.Run(() => exchanges);
        }
        public static Task<List<ExchangeViewModel>> getExchangeByUserId(int id)
        {
            var exchanges = new List<ExchangeViewModel>
            {
                new ExchangeViewModel{Name="Exchange 1",Enabled=DateTime.Now,EnteredApi="EnteredApi 1",VerifiedConnection="Verified connection 1"},
                new ExchangeViewModel{Name="Exchange 2",Enabled=DateTime.Now,EnteredApi="EnteredApi 2",VerifiedConnection="Verified connection 2"},
                new ExchangeViewModel{Name="Exchange 3",Enabled=DateTime.Now,EnteredApi="EnteredApi 3",VerifiedConnection="Verified connection 3"},
                new ExchangeViewModel{Name="Exchange 4",Enabled=DateTime.Now,EnteredApi="EnteredApi 4",VerifiedConnection="Verified connection 4"},
            };
            return Task.Run(() => exchanges);
        }
    }
}
