using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TickettingSystem.DTOs
{
    public class TradeDTO
    {
        public TradeDTO()
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
