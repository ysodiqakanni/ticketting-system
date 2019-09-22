using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModel
{
    public partial class TradeLog
    {
        public int TradeId { get; set; }
        public int TraderUid { get; set; }
        public int? ExchangeFromTypeId { get; set; }
        public int? ExchangeToTypeId { get; set; }
        public string CurrencyPair { get; set; }
        public int? TradeTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TradePlaceDate { get; set; }
        public string TradeStatus { get; set; }
        public bool WasArbitrageSuggestion { get; set; }  // was short
        public string TradeBcid { get; set; }
        public bool Socialtrade { get; set; }  // was short
        public int? Socialtradetraderid { get; set; }
        public decimal? Price { get; set; }
    }
}
