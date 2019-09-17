using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class TradeFollow
    {
        public int TraderUid { get; set; }
        public int? ExchangeFromTypeId { get; set; }
        public int? ExchangeToTypeId { get; set; }
        public string CurrencyPair { get; set; }
        public int? TradeTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TradePlaceDate { get; set; }
        public string TradeStatus { get; set; }
        public short WasArbitrageSuggestion { get; set; }
        public string TradeBcid { get; set; }
    }
}
