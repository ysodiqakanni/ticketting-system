using System;
namespace TickettingSystem.Models
{
    public class TradeSearchModel
    {
        public TradeSearchModel()
        {
        }
        public int UserId { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public String Exchange { get; set; }
    }
}
