using System;
namespace TickettingSystem.Models
{
    public class TradeViewModel
    {
        public TradeViewModel()
        {
        }
        public int ID { get; set; }
        public Double Price { get; set; }
        public String Operation { get; set; }
        public String Exchange { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UserId { get; set; }
    }
}
