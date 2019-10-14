using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;

namespace TickettingSystem.Api.DTO
{
    public class TradeResponseDTO
    {
        public int ID { get; set; }  // = TradeId
        public decimal Price { get; set; }
        public String CurrencyPair { get; set; }
        public String Operation { get; set; }
        public String Exchange { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }  // = TraderUid
        public bool Arbitrage { get; set; }
        public bool Social { get; set; }
    }
    public class TradeMapper
    {
        private static ITradeService _tradeService;

        public static TradeResponseDTO MapTradeLogToDto(TradeLog trade, ITradeService tradeService)
        {
            _tradeService = tradeService;

            var result = new TradeResponseDTO
            {
                ID = trade.TradeId,
                CreatedOn = trade.TradePlaceDate,
                Price = trade.Amount,
                UserId = trade.TraderUid, 
                Arbitrage = trade.WasArbitrageSuggestion,
                Social = trade.Socialtrade,
                CurrencyPair = trade.CurrencyPair
            };
            result.Exchange = _tradeService.GetExchangeTypeById(trade.ExchangeFromTypeId.Value) + "/" + _tradeService.GetExchangeTypeById(trade.ExchangeToTypeId.Value);
            result.Operation = _tradeService.GetTradeOperationById(trade.TradeTypeId.Value);
            return result;
        } 
    }
}
