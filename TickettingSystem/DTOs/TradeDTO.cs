using System;
namespace TickettingSystem.DTOs
{
    public class TradeDTO
    {
        public TradeDTO()
        {
        }
        public int ID { get; set; }  // = TradeId
        public decimal Price { get; set; }
        public String Operation { get; set; }
        public String Exchange { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }  // = TraderUid

        public bool Arbitrage { get; set; }
        public bool Social { get; set; }

        // public int TradeId { get; set; }
        //// public int TraderUid { get; set; }
        //public int? ExchangeFromTypeId { get; set; }
        //public int? ExchangeToTypeId { get; set; }
        //public string CurrencyPair { get; set; }
        //public int? TradeTypeId { get; set; }
        //public decimal Amount { get; set; }
        //public DateTime TradePlaceDate { get; set; }
        //public string TradeStatus { get; set; }
        //public bool WasArbitrageSuggestion { get; set; }
        //public string TradeBcid { get; set; }
        //public bool Socialtrade { get; set; }
        //public int? Socialtradetraderid { get; set; }
        // public decimal? Price { get; set; }
    }
}