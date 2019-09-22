using System;
using System.Collections.Generic;
using System.Text;
using static TickettingSystem.Core.Exchange;

namespace TickettingSystem.Core
{
    public class Trade: Entity
    {
        public Double Price { get; set; }
        public String Operation { get; set; }
        public String Exchange { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UserId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
