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
        public String CurrencyPair { get; set; }
    }
}