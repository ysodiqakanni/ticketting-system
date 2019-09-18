using System;
using System.Collections.Generic;

namespace TickettingSystem.Data.DbModels
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
        public short WasArbitrageSuggestion { get; set; }
        public string TradeBcid { get; set; }
        public short Socialtrade { get; set; }
        public int? Socialtradetraderid { get; set; }
        public decimal? Price { get; set; }
    }
}
