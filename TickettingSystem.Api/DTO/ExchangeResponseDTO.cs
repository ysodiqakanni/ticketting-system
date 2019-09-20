using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.DTO
{
    public class ExchangeResponseDTO
    {
        public int ID { get; set; } 
        public string ExchangeName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string APIsEntered { get; set; }
    }

    public class ExchangeMapper
    {
        private static IExchangeService _exchangeService;
        public static ExchangeResponseDTO MapExchangeDetailsToDto(Exchangesusers exchangeDetails, IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
            string count = string.Empty;
            var result = new ExchangeResponseDTO
            {
                ID = exchangeDetails.EuId,
                DateEnabled = exchangeDetails.ExchangeuCrDt,
            };
            result.ExchangeName = _exchangeService.GetExchangeTypeById(exchangeDetails.ExchangeuId);
            result.APIsEntered = _exchangeService.GetApiEnteredCount(exchangeDetails.EuId);

            return result;
        }
    }
}